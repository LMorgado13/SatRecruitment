using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.DTOs;
using Sat.Recruitment.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Sat.Recruitment.Api.Clases;
using Microsoft.AspNetCore.Http;
using System.IO;



namespace Sat.Recruitment.Api.Controllers
{
    

    [ApiController]
    [Route("api/[controller]")]
    public partial class UsersController : ControllerBase
    {
        private readonly List<ClsUser> _users = new List<ClsUser>();
        public UsersController()
        {
        }


        /// <summary>
        /// create-user
        /// </summary>
        /// <param name="{objResp.Name} {objResp.Email} {objResp.Phone} {objResp.Address} {objResp.UserType} {objResp.Money}"></param>
        /// <returns>ClsResultInfo</returns>

        [HttpPost]
        [Route("create-user")]
        public async Task<ClsResultInfo> CreateUser([FromBody] ClsCreateUsersDTO ObjIn)
        {
            ClsResultInfo ObjResult = new ClsResultInfo();
            ClsUser objResp = new ClsUser();

            ClsCreateUser objCU = new ClsCreateUser();

            try
            {

                objResp = objCU.CreateUserIn(ObjIn);

                var reader = ReadUsersFromFile();

               

                //Normalize email
                var aux = objResp.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

                var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

                aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

                objResp.Email = string.Join("@", new string[] { aux[0], aux[1] });

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    var user = new ClsUser
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    _users.Add(user);
                }
                reader.Close();

                try
                {
                    var isDuplicated = false;
                    foreach (var user in _users)
                    {
                        if (user.Email == objResp.Email
                            ||
                            user.Phone == objResp.Phone)
                        {
                            isDuplicated = true;
                        }
                        else if (user.Name == objResp.Name)
                        {
                            if (user.Address == objResp.Address)
                            {
                                isDuplicated = true;
                                throw new Exception("User is duplicated");
                            }

                        }
                    }

                    if (!isDuplicated)
                    {
                        Debug.WriteLine("User Created");
                        
                        Stream fs = new FileStream("./Files/Users.txt", FileMode.Open, FileAccess.Read);
                        StreamReader objRead = new StreamReader(fs);

                        string userNew = $"{objResp.Name},{objResp.Email},{objResp.Phone},{objResp.Address},{objResp.UserType},{objResp.Money}";
                        string line = objRead.ReadToEnd();
                        string ln = "\r\n";
                        string alluser = $"{line}{ln}{userNew}";
                        fs.Close();

                        using (Stream fc = new FileStream("./Files/Users.txt", FileMode.Create, FileAccess.Write)) 
                        {
                            using (StreamWriter objWrite = new StreamWriter(fc))
                            {
                                objWrite.WriteLine(alluser);
                            }
                            
                        }
                     
                        return  new ClsResultInfo()
                        {
                            IsSuccess = true,
                            Errors = "User Created"
                        };
                    }
                    else
                    {
                        Debug.WriteLine("The user is duplicated");

                        return new ClsResultInfo()
                        {
                            IsSuccess = false,
                            Errors = "The user is duplicated"
                        };
                    }
                }
                catch
                {
                    Debug.WriteLine("The user is duplicated");
                    return new ClsResultInfo()
                    {
                        IsSuccess = false,
                        Errors = "The user is duplicated"
                    };
                }



            }
            catch (Exception ex)
            {

                ObjResult.IsSuccess = false;
                ObjResult.Errors = ex.Message;
                ObjResult.ErrorNum = Convert.ToInt32(StatusCode(StatusCodes.Status500InternalServerError).StatusCode);
            }

            return ObjResult;

        }


        /// <summary>
        /// get-user
        /// </summary>
        /// <param name=""></param>
        /// <returns>ClsResultInfo</returns>
        
        [HttpGet]
        [Route("get-user")]
        public async Task<List<ClsUser>> GetUser()
        {
            ClsResultInfo ObjResult = new ClsResultInfo();
            List<ClsUser> objResp = new List<ClsUser>();

            try
            {

                var reader = ReadUsersFromFile();

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLineAsync().Result;
                    var user = new ClsUser
                    {
                        Name = line.Split(',')[0].ToString(),
                        Email = line.Split(',')[1].ToString(),
                        Phone = line.Split(',')[2].ToString(),
                        Address = line.Split(',')[3].ToString(),
                        UserType = line.Split(',')[4].ToString(),
                        Money = decimal.Parse(line.Split(',')[5].ToString()),
                    };
                    _users.Add(user);
                }

                objResp = _users;
                reader.Close();
                
            }
            catch (Exception ex)
            {

                ObjResult.IsSuccess = false;
                ObjResult.Errors = ex.Message;
                ObjResult.ErrorNum = Convert.ToInt32(StatusCode(StatusCodes.Status500InternalServerError).StatusCode);
            }

            return objResp;

        }

    }
   
}
