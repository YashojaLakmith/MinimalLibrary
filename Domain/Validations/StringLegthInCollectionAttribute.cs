using System.ComponentModel.DataAnnotations;

namespace Domain.Validations
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.Field, AllowMultiple = false)]
    internal sealed class StringLegthInCollectionAttribute : ValidationAttribute
    {
        private readonly int _minLength;
        private readonly int _maxLength;

        internal StringLegthInCollectionAttribute(int minLength, int maxLength)
        {
            _minLength = minLength;
            _maxLength = maxLength;
        }

        public override bool IsValid(object? value)
        {
            var strings = value as IEnumerable<string>;
            if(strings is null)
            {
                return false;
            }

            foreach(var s in strings)
            {
                if(s.Length < _minLength || s.Length > _maxLength)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
