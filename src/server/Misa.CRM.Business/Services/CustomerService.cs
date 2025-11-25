using AutoMapper;
using Microsoft.AspNetCore.Http;
using Misa.CRM.Business.Common.Models;
using Misa.CRM.Business.Dtos.Customer;
using Misa.CRM.Business.Entities.Common;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Business.Interfaces.Services;

namespace Misa.CRM.Business.Services;

public class CustomerService : BaseService<Customer, CustomerDto, CustomerCreateUpdateDto>, ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    public CustomerService(ICustomerRepository repository, IMapper mapper) : base(repository, mapper)
    {
        _customerRepository = repository;
    }

    public IEnumerable<CustomerDto> GetCustomersWithType()
    {
        return _mapper.Map<IEnumerable<CustomerDto>>(_customerRepository.GetCustomersWithTypeAsync());
    }

    public string GetNewCustomerId()
    {
        return _customerRepository.GetNewCustomerId();
    }

    public ImportResult Import(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var dtos = new List<CustomerCreateUpdateDto>();
        var errors = new List<ImportErrorRow>();

        int rowIndex = 0;
        string? line;
        bool isHeader = true;

        while ((line = reader.ReadLine()) != null)
        {
            rowIndex++;

            // Bỏ qua header
            if (isHeader)
            {
                isHeader = false;
                continue;
            }

            var cols = line.Split(',').Select(x => x.Trim()).ToArray();

            try
            {
                var dto = new CustomerCreateUpdateDto
                {
                    CustomerName = cols.Length > 0 ? cols[0] : throw new ArgumentException("CustomerName is required"),
                    CustomerAddress = cols.Length > 1 ? cols[1] : null,
                    CustomerPhone = cols.Length > 2 && !string.IsNullOrWhiteSpace(cols[2]) ? cols[2].Replace(" ", "") : null,
                    CustomerEmail = cols.Length > 3 && !string.IsNullOrWhiteSpace(cols[3]) ? cols[3] : null,
                    CustomerTaxCode = cols.Length > 4 ? cols[4] : null,
                    CustomerTypeId = cols.Length > 5 && Guid.TryParse(cols[5], out var g) ? g : null,
                    CustomerIndustry = cols.Length > 6 ? cols[6] : null,
                    Gender = cols.Length > 7 && byte.TryParse(cols[7], out var b) ? b : null,
                    OtherPhoneNumber = cols.Length > 8 ? cols[8].Replace(" ", "") : null,
                    LastPurchaseDate = cols.Length > 9 && DateTime.TryParse(cols[9], out var dt) ? dt : null,
                    PurchaseItems = cols.Length > 10 ? cols[10] : null,
                    PurchaseItemName = cols.Length > 11 ? cols[11] : null,
                    ShippingAddress = cols.Length > 12 ? cols[12] : null
                };

                dtos.Add(dto);
            }
            catch (Exception ex)
            {
                errors.Add(new ImportErrorRow
                {
                    RowIndex = rowIndex,
                    Error = ex.Message
                });
            }
        }

        // Map DTO → Entity
        var customers = _mapper.Map<List<Customer>>(dtos);
        
        var importResult = _customerRepository.Import(customers);

        return importResult;
    }


    public PaginatedResult<CustomerDto> Paginate(string? search, int pageIndex, int pageSize, string? sortColumn, int sortDirection, Guid? customerTypeId)
    {
        var result = _customerRepository.SearchAndPaginate(search, pageIndex, pageSize, sortColumn, sortDirection, customerTypeId);

        return new PaginatedResult<CustomerDto>(
            result.PageIndex,
            result.PageSize,
            result.TotalRecords,
            [.. _mapper.Map<IEnumerable<CustomerDto>>(result.Items)]
        );
    }

    public int CheckEmailUnique(string email)
    {
        return _customerRepository.CheckEmailUnique(email);
    }

    public int CheckPhoneUnique(string phone)
    {
        return _customerRepository.CheckPhoneUnique(phone);
    }

    public int ChangeCustomerType(List<string> ids, Guid? customerTypeId)
    {
        return _customerRepository.ChangeCustomerType(ids, customerTypeId);
    }
}
