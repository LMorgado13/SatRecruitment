using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.IO;

namespace Sat.Recruitment.Api.Clases
{
    public class ClsCreateUser
    {
        private readonly List<ClsUser> _users = new List<ClsUser>();
        public ClsCreateUser()
        {

        }
        public ClsUser CreateUserIn(ClsCreateUsersDTO ObjIn)
        {
            return CreateUser(ObjIn.Name, ObjIn.Email, ObjIn.Address, ObjIn.Phone, ObjIn.UserType, Convert.ToString(ObjIn.Money).ToString());
        }

        public ClsUser CreateUser(string name, string email, string address, string phone, string userType, string money)
        {
            var newUser = new ClsUser
            {
                Name = name,
                Email = email,
                Address = address,
                Phone = phone,
                UserType = userType,
                Money = Convert.ToDecimal(money)
            };

            if (newUser.UserType == "Normal")
            {
                if (Convert.ToDecimal(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    var gif = Convert.ToDecimal(money) * percentage;
                    newUser.Money = newUser.Money + gif;
                }

                if (Convert.ToDecimal(money) < 100)
                {
                    if (Convert.ToDecimal(money) > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = Convert.ToDecimal(money) * percentage;
                        newUser.Money = newUser.Money + gif;
                    }
                }
            }
            if (newUser.UserType == "SuperUser")
            {
                if (Convert.ToDecimal(money) > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = Convert.ToDecimal(money) * percentage;
                    newUser.Money = newUser.Money + gif;
                }
            }
            if (newUser.UserType == "Premium")
            {
                if (Convert.ToDecimal(money) > 100)
                {
                    var gif = Convert.ToDecimal(money) * 2;
                    newUser.Money = newUser.Money + gif;
                }
            }

            return newUser;

        }
    }
}
