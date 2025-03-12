using System.Linq.Expressions;

namespace Faces.Domain.Util
{
    public static class Validations
    {
        public static bool IsDefault<T, TProp>(T obj, Expression<Func<T, TProp>> propertyExpression)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj));

            if (propertyExpression == null)
                throw new ArgumentNullException(nameof(propertyExpression));

            if (propertyExpression.Body is MemberExpression memberExpr)
            {
                var propertyName = memberExpr.Member.Name;
                var propertyValue = typeof(T).GetProperty(propertyName)?.GetValue(obj);

                var defaultValue = default(TProp);

                return Equals(propertyValue, defaultValue);
            }

            throw new ArgumentException("Invalid property expression");
        }

    }
}
