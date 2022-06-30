using ACTINEO.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace ACTINEO.Core {
    public static class SortExtensions {
        public static IQueryable<T> SortByFieldName<T>(this IQueryable<T> target, string sortPropertyName, SortDirection direction) {
            if (!String.IsNullOrEmpty(sortPropertyName)) {
                Expression<Func<T, object>> sortExpression = GetSortLambda<T>(sortPropertyName);
                switch (direction) {
                    case SortDirection.Asc:
                        return target.AsQueryable<T>().OrderBy(sortExpression);
                    case SortDirection.Desc:
                        return target.AsQueryable<T>().OrderByDescending(sortExpression);
                    default:
                        return target;
                }
            }
            return target;
        }
        private static Expression<Func<T, object>> GetSortLambda<T>(string propertyPath) {
            var param = Expression.Parameter(typeof(T), "p");
            var parts = propertyPath.Split('.');
            Expression parent = param;
            foreach (var part in parts) {
                parent = Expression.Property(parent, part);
            }

            if (parent.Type.IsValueType) {
                var converted = Expression.Convert(parent, typeof(object));
                return Expression.Lambda<Func<T, object>>(converted, param);
            }
            else {
                return Expression.Lambda<Func<T, object>>(parent, param);
            }
        }
       
        
    }
}
