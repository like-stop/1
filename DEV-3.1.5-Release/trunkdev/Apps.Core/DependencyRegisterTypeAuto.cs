﻿
using Unity.Attributes;using Unity;
namespace Apps.Core
{
    public partial class DependencyRegisterType
    {
        public static void Container(ref UnityContainer container)
        {
 //------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

			container.RegisterType<Apps.IBLL.Flow.IFlow_ExternalBLL, Apps.BLL.Flow.Flow_ExternalBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_ExternalRepository, Apps.DAL.Flow.Flow_ExternalRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_FormBLL, Apps.BLL.Flow.Flow_FormBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_FormRepository, Apps.DAL.Flow.Flow_FormRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_FormAttrBLL, Apps.BLL.Flow.Flow_FormAttrBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_FormAttrRepository, Apps.DAL.Flow.Flow_FormAttrRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_FormContentBLL, Apps.BLL.Flow.Flow_FormContentBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_FormContentRepository, Apps.DAL.Flow.Flow_FormContentRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_FormContentStepCheckBLL, Apps.BLL.Flow.Flow_FormContentStepCheckBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_FormContentStepCheckRepository, Apps.DAL.Flow.Flow_FormContentStepCheckRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_FormContentStepCheckStateBLL, Apps.BLL.Flow.Flow_FormContentStepCheckStateBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_FormContentStepCheckStateRepository, Apps.DAL.Flow.Flow_FormContentStepCheckStateRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_SealBLL, Apps.BLL.Flow.Flow_SealBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_SealRepository, Apps.DAL.Flow.Flow_SealRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_StepBLL, Apps.BLL.Flow.Flow_StepBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_StepRepository, Apps.DAL.Flow.Flow_StepRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_StepRuleBLL, Apps.BLL.Flow.Flow_StepRuleBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_StepRuleRepository, Apps.DAL.Flow.Flow_StepRuleRepository>();
			container.RegisterType<Apps.IBLL.Flow.IFlow_TypeBLL, Apps.BLL.Flow.Flow_TypeBLL>();
			container.RegisterType<Apps.IDAL.Flow.IFlow_TypeRepository, Apps.DAL.Flow.Flow_TypeRepository>();
			container.RegisterType<Apps.IBLL.JOB.IJOB_TASKJOBSBLL, Apps.BLL.JOB.JOB_TASKJOBSBLL>();
			container.RegisterType<Apps.IDAL.JOB.IJOB_TASKJOBSRepository, Apps.DAL.JOB.JOB_TASKJOBSRepository>();
			container.RegisterType<Apps.IBLL.JOB.IJOB_TASKJOBS_LOGBLL, Apps.BLL.JOB.JOB_TASKJOBS_LOGBLL>();
			container.RegisterType<Apps.IDAL.JOB.IJOB_TASKJOBS_LOGRepository, Apps.DAL.JOB.JOB_TASKJOBS_LOGRepository>();
			container.RegisterType<Apps.IBLL.MIS.IMIS_ArticleBLL, Apps.BLL.MIS.MIS_ArticleBLL>();
			container.RegisterType<Apps.IDAL.MIS.IMIS_ArticleRepository, Apps.DAL.MIS.MIS_ArticleRepository>();
			container.RegisterType<Apps.IBLL.MIS.IMIS_Article_AlbumsBLL, Apps.BLL.MIS.MIS_Article_AlbumsBLL>();
			container.RegisterType<Apps.IDAL.MIS.IMIS_Article_AlbumsRepository, Apps.DAL.MIS.MIS_Article_AlbumsRepository>();
			container.RegisterType<Apps.IBLL.MIS.IMIS_Article_CategoryBLL, Apps.BLL.MIS.MIS_Article_CategoryBLL>();
			container.RegisterType<Apps.IDAL.MIS.IMIS_Article_CategoryRepository, Apps.DAL.MIS.MIS_Article_CategoryRepository>();
			container.RegisterType<Apps.IBLL.MIS.IMIS_Article_CommentBLL, Apps.BLL.MIS.MIS_Article_CommentBLL>();
			container.RegisterType<Apps.IDAL.MIS.IMIS_Article_CommentRepository, Apps.DAL.MIS.MIS_Article_CommentRepository>();
			container.RegisterType<Apps.IBLL.MIS.IMIS_WebIM_CommonTalkBLL, Apps.BLL.MIS.MIS_WebIM_CommonTalkBLL>();
			container.RegisterType<Apps.IDAL.MIS.IMIS_WebIM_CommonTalkRepository, Apps.DAL.MIS.MIS_WebIM_CommonTalkRepository>();
			container.RegisterType<Apps.IBLL.MIS.IMIS_WebIM_MessageBLL, Apps.BLL.MIS.MIS_WebIM_MessageBLL>();
			container.RegisterType<Apps.IDAL.MIS.IMIS_WebIM_MessageRepository, Apps.DAL.MIS.MIS_WebIM_MessageRepository>();
			container.RegisterType<Apps.IBLL.MIS.IMIS_WebIM_Message_RecBLL, Apps.BLL.MIS.MIS_WebIM_Message_RecBLL>();
			container.RegisterType<Apps.IDAL.MIS.IMIS_WebIM_Message_RecRepository, Apps.DAL.MIS.MIS_WebIM_Message_RecRepository>();
			container.RegisterType<Apps.IBLL.MIS.IMIS_WebIM_RecentContactBLL, Apps.BLL.MIS.MIS_WebIM_RecentContactBLL>();
			container.RegisterType<Apps.IDAL.MIS.IMIS_WebIM_RecentContactRepository, Apps.DAL.MIS.MIS_WebIM_RecentContactRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_ContactCompanyBLL, Apps.BLL.Spl.Spl_ContactCompanyBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_ContactCompanyRepository, Apps.DAL.Spl.Spl_ContactCompanyRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_ContactCompanyCategoryBLL, Apps.BLL.Spl.Spl_ContactCompanyCategoryBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_ContactCompanyCategoryRepository, Apps.DAL.Spl.Spl_ContactCompanyCategoryRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_InOutCategoryBLL, Apps.BLL.Spl.Spl_InOutCategoryBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_InOutCategoryRepository, Apps.DAL.Spl.Spl_InOutCategoryRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_PersonBLL, Apps.BLL.Spl.Spl_PersonBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_PersonRepository, Apps.DAL.Spl.Spl_PersonRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_ProductBLL, Apps.BLL.Spl.Spl_ProductBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_ProductRepository, Apps.DAL.Spl.Spl_ProductRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_ProductCategoryBLL, Apps.BLL.Spl.Spl_ProductCategoryBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_ProductCategoryRepository, Apps.DAL.Spl.Spl_ProductCategoryRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_WareCategoryBLL, Apps.BLL.Spl.Spl_WareCategoryBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_WareCategoryRepository, Apps.DAL.Spl.Spl_WareCategoryRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_WareDetailsBLL, Apps.BLL.Spl.Spl_WareDetailsBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_WareDetailsRepository, Apps.DAL.Spl.Spl_WareDetailsRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_WarehouseBLL, Apps.BLL.Spl.Spl_WarehouseBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_WarehouseRepository, Apps.DAL.Spl.Spl_WarehouseRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_WarehouseCategoryBLL, Apps.BLL.Spl.Spl_WarehouseCategoryBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_WarehouseCategoryRepository, Apps.DAL.Spl.Spl_WarehouseCategoryRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_WarehouseWarrantBLL, Apps.BLL.Spl.Spl_WarehouseWarrantBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_WarehouseWarrantRepository, Apps.DAL.Spl.Spl_WarehouseWarrantRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_WarehouseWarrantDetailsBLL, Apps.BLL.Spl.Spl_WarehouseWarrantDetailsBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_WarehouseWarrantDetailsRepository, Apps.DAL.Spl.Spl_WarehouseWarrantDetailsRepository>();
			container.RegisterType<Apps.IBLL.Spl.ISpl_WareUnitConvertBLL, Apps.BLL.Spl.Spl_WareUnitConvertBLL>();
			container.RegisterType<Apps.IDAL.Spl.ISpl_WareUnitConvertRepository, Apps.DAL.Spl.Spl_WareUnitConvertRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysAreasBLL, Apps.BLL.Sys.SysAreasBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysAreasRepository, Apps.DAL.Sys.SysAreasRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysCalendarPlanBLL, Apps.BLL.Sys.SysCalendarPlanBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysCalendarPlanRepository, Apps.DAL.Sys.SysCalendarPlanRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysExceptionBLL, Apps.BLL.Sys.SysExceptionBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysExceptionRepository, Apps.DAL.Sys.SysExceptionRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysLogBLL, Apps.BLL.Sys.SysLogBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysLogRepository, Apps.DAL.Sys.SysLogRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysModuleBLL, Apps.BLL.Sys.SysModuleBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysModuleRepository, Apps.DAL.Sys.SysModuleRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysModuleDataFilterBLL, Apps.BLL.Sys.SysModuleDataFilterBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysModuleDataFilterRepository, Apps.DAL.Sys.SysModuleDataFilterRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysModuleOperateBLL, Apps.BLL.Sys.SysModuleOperateBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysModuleOperateRepository, Apps.DAL.Sys.SysModuleOperateRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysPositionBLL, Apps.BLL.Sys.SysPositionBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysPositionRepository, Apps.DAL.Sys.SysPositionRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysRightBLL, Apps.BLL.Sys.SysRightBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysRightRepository, Apps.DAL.Sys.SysRightRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysRightDataFilterBLL, Apps.BLL.Sys.SysRightDataFilterBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysRightDataFilterRepository, Apps.DAL.Sys.SysRightDataFilterRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysRightOperateBLL, Apps.BLL.Sys.SysRightOperateBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysRightOperateRepository, Apps.DAL.Sys.SysRightOperateRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysRoleBLL, Apps.BLL.Sys.SysRoleBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysRoleRepository, Apps.DAL.Sys.SysRoleRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysSampleBLL, Apps.BLL.Sys.SysSampleBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysSampleRepository, Apps.DAL.Sys.SysSampleRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysSettingsBLL, Apps.BLL.Sys.SysSettingsBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysSettingsRepository, Apps.DAL.Sys.SysSettingsRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysStructBLL, Apps.BLL.Sys.SysStructBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysStructRepository, Apps.DAL.Sys.SysStructRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysUserBLL, Apps.BLL.Sys.SysUserBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysUserRepository, Apps.DAL.Sys.SysUserRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysUserConfigBLL, Apps.BLL.Sys.SysUserConfigBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysUserConfigRepository, Apps.DAL.Sys.SysUserConfigRepository>();
			container.RegisterType<Apps.IBLL.Sys.ISysValidTokenBLL, Apps.BLL.Sys.SysValidTokenBLL>();
			container.RegisterType<Apps.IDAL.Sys.ISysValidTokenRepository, Apps.DAL.Sys.SysValidTokenRepository>();
			container.RegisterType<Apps.IBLL.WC.IWC_GroupBLL, Apps.BLL.WC.WC_GroupBLL>();
			container.RegisterType<Apps.IDAL.WC.IWC_GroupRepository, Apps.DAL.WC.WC_GroupRepository>();
			container.RegisterType<Apps.IBLL.WC.IWC_MessageResponseBLL, Apps.BLL.WC.WC_MessageResponseBLL>();
			container.RegisterType<Apps.IDAL.WC.IWC_MessageResponseRepository, Apps.DAL.WC.WC_MessageResponseRepository>();
			container.RegisterType<Apps.IBLL.WC.IWC_OfficalAccountsBLL, Apps.BLL.WC.WC_OfficalAccountsBLL>();
			container.RegisterType<Apps.IDAL.WC.IWC_OfficalAccountsRepository, Apps.DAL.WC.WC_OfficalAccountsRepository>();
			container.RegisterType<Apps.IBLL.WC.IWC_ResponseLogBLL, Apps.BLL.WC.WC_ResponseLogBLL>();
			container.RegisterType<Apps.IDAL.WC.IWC_ResponseLogRepository, Apps.DAL.WC.WC_ResponseLogRepository>();
			container.RegisterType<Apps.IBLL.WC.IWC_UserBLL, Apps.BLL.WC.WC_UserBLL>();
			container.RegisterType<Apps.IDAL.WC.IWC_UserRepository, Apps.DAL.WC.WC_UserRepository>();
        }
    }
}