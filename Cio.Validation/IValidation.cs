using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cio.Validation
{
    public interface IValidation
    {
        ValidationResult Validate(ValidationContext context);
    }
}
