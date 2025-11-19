using System.Reflection;
using Misa.CRM.Business.MisaAttributes;

namespace Misa.CRM.Business.Helpers;

public static class DapperMetadataHelper
{
    /// <summary>
    /// Lấy tên bảng của entity
    /// </summary>
    public static string GetTableName<T>()
    {
        var type = typeof(T);
        var tableAttr = type.GetCustomAttribute<MisaTableAttribute>();
        return tableAttr?.TableName ?? type.Name;
    }

    /// <summary>
    /// Lấy tất cả mapping property -> column
    /// Chỉ lấy những property có attribute hoặc override
    /// </summary>
    public static Dictionary<string, string> GetColumnMappings<T>()
    {
        var mappings = new Dictionary<string, string>();
        var type = typeof(T);

        // Lấy override từ class
        var overrides = type.GetCustomAttributes<MisaColumnOverrideAttribute>()
                            .ToDictionary(o => o.PropertyName, o => o.ColumnName);

        // Lấy property public instance
        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var prop in props)
        {
            // Ưu tiên override
            if (overrides.TryGetValue(prop.Name, out var overrideCol))
            {
                mappings[prop.Name] = overrideCol;
                continue;
            }

            // Lấy MisaColumnAttribute nếu có
            var colAttr = prop.GetCustomAttribute<MisaColumnAttribute>();
            if (colAttr != null)
            {
                mappings[prop.Name] = colAttr.ColumnName;
            }
        }

        return mappings;
    }

    /// <summary>
    /// Chỉ lấy danh sách property có attribute (MisaColumn / override)
    /// </summary>
    public static IEnumerable<PropertyInfo> GetColumnProperties<T>()
    {
        var type = typeof(T);
        var overrides = type.GetCustomAttributes<MisaColumnOverrideAttribute>()
                            .Select(o => o.PropertyName)
                            .ToHashSet();

        var props = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        return props.Where(p =>
            overrides.Contains(p.Name) || p.GetCustomAttribute<MisaColumnAttribute>() != null
        );
    }

    /// <summary>
    /// Lấy tên khóa chính của cột
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetPrimaryKey<T>()
    {
        var type = typeof(T);

        var pkProperty = type.GetProperties()
                             .FirstOrDefault(p => p.GetCustomAttribute<MisaPrimaryKeyAttribute>() != null);

        if (pkProperty != null)
        {
            var attr = pkProperty.GetCustomAttribute<MisaPrimaryKeyAttribute>();
            return attr?.PrimaryKeyName ?? pkProperty.Name;
        }

        return "Id";
    }

    /// <summary>
    /// Lấy tên khóa chính của entity
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static string GetPrimaryKeyProperty<T>()
    {
        var type = typeof(T);

        var pkProperty = type.GetProperties()
                             .FirstOrDefault(p => p.GetCustomAttribute<MisaPrimaryKeyAttribute>() != null);

        if (pkProperty != null)
            return pkProperty.Name;

        return "Id";
    }
}
