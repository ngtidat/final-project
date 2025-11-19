using System.Reflection;
using AutoMapper;
using Misa.CRM.Business.Common.Exceptions;
using Misa.CRM.Business.Entities;
using Misa.CRM.Business.Helpers;
using Misa.CRM.Business.Interfaces.Repositories;
using Misa.CRM.Business.Interfaces.Services;
using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Services;

public class BaseService<T, TDto, TCreateUpdateDto> : IBaseService<T, TDto, TCreateUpdateDto> where TDto : class where T : BaseEntity where TCreateUpdateDto : class
{
    protected readonly IBaseRepository<T> _repository;

    protected IMapper _mapper;

    public BaseService(IBaseRepository<T> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public int Add(TCreateUpdateDto dto)
    {
        var entity = _mapper.Map<T>(dto);
        ValidateEntity(entity);
        return _repository.Add(entity);
    }

    public int Delete(string id, bool isHardDelete = false)
    {
        return _repository.Delete(id, isHardDelete);
    }

    public int Delete(IEnumerable<string> ids, bool isHardDelete = false)
    {
        return _repository.Delete(ids, isHardDelete);
    }

    public IEnumerable<TDto> GetAll()
    {
        return _mapper.Map<IEnumerable<TDto>>(_repository.GetAll());
    }

    public TDto GetById(string id)
    {
        return _mapper.Map<TDto>(_repository.GetById(id));
    }

    public int Update(string id, TCreateUpdateDto dto)
    {
        var entity = _mapper.Map<T>(dto);

        var pkProp = DapperMetadataHelper.GetPrimaryKeyProperty<T>();
        typeof(T).GetProperty(pkProp)?.SetValue(entity, id);

        ValidateEntity(entity);
        return _repository.Update(entity);
    }

    public void ValidateEntity(T entity)
    {
        var properties = typeof(T).GetProperties();

        foreach (var prop in properties)
        {
            var value = prop.GetValue(entity);

            // Required
            var requiredAttr = prop.GetCustomAttribute<MisaRequiredAttribute>();
            if (requiredAttr != null)
            {
                if (value == null || (value is string strValue && string.IsNullOrWhiteSpace(strValue)))
                {
                    throw new RequestValidationException(requiredAttr.ErrorMessage ?? $"{prop.Name} is required");
                }
            }

            // MaxLength
            var maxLengthAttr = prop.GetCustomAttribute<MisaMaxLengthAttribute>();
            if (maxLengthAttr != null && value is string maxLengthValue &&
                maxLengthValue.Length > maxLengthAttr.MaxLength)
            {
                throw new RequestValidationException(maxLengthAttr.ErrorMessage);
            }

            // Email
            var emailAttr = prop.GetCustomAttribute<MisaEmailAttribute>();
            if (emailAttr != null && value is string emailValue &&
                !emailAttr.IsValid(emailValue))
            {
                throw new RequestValidationException(emailAttr.ErrorMessage);
            }

            // Phone
            var phoneAttr = prop.GetCustomAttribute<MisaPhoneAttribute>();
            if (phoneAttr != null && value is string phoneValue &&
                !phoneAttr.IsValid(phoneValue))
            {
                throw new RequestValidationException(phoneAttr.ErrorMessage);
            }


            // Unique
            // var uniqueAttr = prop.GetCustomAttribute<MisaUniqueAttribute>();
            // if (uniqueAttr != null)
            // {
            //     var tableName = DapperMetadataHelper.GetTableName<T>();
            //     var isExist = _repository.CheckUnique(
            //         tableName,
            //         prop.Name.ToSnakeCase(),
            //         value?.ToString() ?? "",
            //         uniqueAttr.PrimaryKeyName,
            //         GetPrimaryKeyValue(entity)
            //     );

            //     if (isExist)
            //         throw new ResourceUniqueException(uniqueAttr.ErrorMessage);
            // }
        }
    }
}
