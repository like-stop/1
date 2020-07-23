using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Apps.Models;
using Apps.IDAL;
using System.Data;
using Apps.Models.Sys;

namespace Apps.DAL.Sys
{
    public partial class SysRoleRepository
    {


        public IQueryable<SysUser> GetRefSysUser( string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                return from m in Context.SysRole
                       from f in m.SysUser
                       where m.Id == id
                       select f;
            }
            return null;
        }
        public List<SysUserByRoleIdModel> GetUserByRoleId(string roleId)
        {

            //获取组织架构的下属部门

            var query = (from m in Context.SysUser
                         select new
                         {
                             m.Id,
                             m.UserName,
                             m.TrueName,
                             Flag = (from r in Context.SysRole
                                     from u in r.SysUser
                                     where r.Id == roleId && u.Id == m.Id
                                     select u).Count() > 0 ? 1 : 0
                         });



            List<SysUserByRoleIdModel> list = new List<SysUserByRoleIdModel>();
            foreach (var r in query)
                list.Add(new SysUserByRoleIdModel()
                {
                    Id = r.Id,
                    UserName = r.UserName,
                    TrueName = r.TrueName,
                    flag = r.Flag

                });
            return list;
        }

        public IQueryable<P_Sys_GetUserByRoleId_Result> GetUserByRoleId(string roleId,string depId)
        {
            return Context.P_Sys_GetUserByRoleId(roleId, depId).AsQueryable();
        }
        
        public void UpdateSysRoleSysUser(string roleId,string[] userIds)
        { 
            using(DBContainer db = new DBContainer())
            {
                db.P_Sys_DeleteSysRoleSysUserByRoleId(roleId);
                foreach (string userid in userIds)
                {
                    if (!string.IsNullOrWhiteSpace(userid))
                    {
                        db.P_Sys_UpdateSysRoleSysUser(roleId, userid);
                    }
                }
                db.SaveChanges();
            }
        }

        public void P_Sys_InsertSysRight()
        {
            Context.P_Sys_InsertSysRight();
        }
        //清理无用的项
        public void P_Sys_ClearUnusedRightOperate() {
            Context.P_Sys_ClearUnusedRightOperate();
        }

    }
}
