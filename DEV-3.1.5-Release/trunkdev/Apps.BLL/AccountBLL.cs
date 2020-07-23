using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apps.IBLL;
using Apps.BLL.Core;
using Unity.Attributes;
using Apps.IDAL;
using Apps.Models;
using Apps.Common;
using Apps.Models.Sys;

namespace Apps.BLL
{
    public class AccountBLL : IAccountBLL
    {
        [Dependency]
        public IAccountRepository accountRepository { get; set; }
        public SysUserModel Login(string username, string pwd)
        {
            SysUser entity = accountRepository.Login(username, pwd);
            if (entity == null)
            {
                return null;
            }
            else
            {
                SysUserModel model = new SysUserModel();
                model.Id = entity.Id;
                model.UserName = entity.UserName;
                model.Password = entity.Password;
                model.TrueName = entity.TrueName;
                model.Card = entity.Card;
                model.MobileNumber = entity.MobileNumber;
                model.Address = entity.Address;
                model.Province = entity.Province;
                model.City = entity.City;
                model.Village = entity.Village;
                model.State = entity.State;
                model.CreateTime = entity.CreateTime;
                model.CreatePerson = entity.CreatePerson;
                model.Sex = entity.Sex;
                model.Birthday = entity.Birthday;
                model.DepId = entity.DepId;
                model.PosId = entity.PosId;
                model.Expertise = entity.Expertise;
                model.Photo = entity.Photo;
                model.Lead = entity.Lead;
                model.LeadName = entity.LeadName;
                return model;
            }

        }
    }
}
