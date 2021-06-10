using System;
using System.Reflection;

namespace Musicalog.Data.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(MemberInfo entityType, params object[] ids) : base(FormatMessage(entityType, ids))
        {
        }
        
        private static string FormatMessage(MemberInfo entityType, params object[] ids)
        {
            var idList = string.Join(',', ids);
            return $"{entityType.Name} with Id '{idList}' was not found.";
        }
    }
}