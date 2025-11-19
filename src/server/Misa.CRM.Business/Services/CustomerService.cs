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
                    CustomerName = cols.Length > 0 ? cols[0] : throw new Exception("CustomerName is required"),
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

        // Validate Entity theo Attribute
        foreach (var c in customers.ToList())
        {
            try
            {
                ValidateEntity(c);
            }
            catch (Exception ex)
            {
                errors.Add(new ImportErrorRow
                {
                    RowIndex = rowIndex,
                    Error = ex.Message
                });

                customers.Remove(c);
            }
        }

        // Bulk Insert vào DB
        var importResult = _customerRepository.Import(customers);

        importResult.Errors.AddRange(errors);
        importResult.Failed = importResult.Errors.Count;
        importResult.Total = customers.Count + importResult.Failed;

        return importResult;
    }


    public PaginatedResult<CustomerDto> Paginate(string? strSearch, int pageIndex, int pageSize, string? sortColumn, int sortDirection)
    {
        var result = _customerRepository.SearchAndPaginate(strSearch, pageIndex, pageSize, sortColumn, sortDirection);

        return new PaginatedResult<CustomerDto>(
            result.PageIndex,
            result.PageSize,
            result.TotalRecords,
            [.. _mapper.Map<IEnumerable<CustomerDto>>(result.Items)]
        );
    }
}
