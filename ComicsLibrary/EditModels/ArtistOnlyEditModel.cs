using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ComicsLibrary.EditModels
{
    public class ArtistOnlyEditModel : TableEditModel
    {
        private string _lastName;
        private string _firstName;
        private string _pseudonym;
        private string _realLastName;
        private string _realFirstName;

        [Required]
        public string LastName
        {
            get => _lastName;
            set
            {
                Set(ref _lastName, value);
                Name = value + ", " + FirstName;
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                Set(ref _firstName, value);
                Name = LastName + ", " + value;
            }
        }

        public override bool Validate(Dictionary<string, List<string>> errors)
        {
            var validate = base.Validate(errors);

            if (Pseudonym is not null && Pseudonym.Equals("yes"))
            {
                if ((RealLastName is null || RealLastName.Length == 0) && (RealFirstName is null || RealFirstName.Length == 0))
                {
                    errors["RealName"] = new List<string>
                    {
                        "Pseudonym requires a real name"
                    };
                    validate = false;
                }
            }

            return validate;
        }

        [Required]
        public string Pseudonym { get => _pseudonym; set => Set(ref _pseudonym, value); }
        public string RealLastName { get => _realLastName; set => Set(ref _realLastName, value); }
        public string RealFirstName { get => _realFirstName; set => Set(ref _realFirstName, value); }
    }
}
