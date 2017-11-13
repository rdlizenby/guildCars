using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Attributes
{
    public class PhoneOrEmailAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is ContactRequest || value is Buyer)
            {
                if (value is ContactRequest)
                { 
                    ContactRequest model = (ContactRequest)value;

                        if (model.Phone == null && model.Email == null)
                        {
                            return false;
                        }

                        return true;
                    
                }
                else
                {
                    Buyer model = (Buyer)value;

                        if (model.Phone == null && model.Email == null)
                        {
                            return false;
                        }

                        return true;
                    
                }
            }

            return false;
        }
    }
}
