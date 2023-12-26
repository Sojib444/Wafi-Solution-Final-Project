using System.Threading.Tasks;
using Kidoo.Learn.Localization;
using Kidoo.Learn.MultiTenancy;
using Kidoo.Learn.Permissions;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Identity.Web.Navigation;
using Volo.Abp.SettingManagement.Web.Navigation;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Kidoo.Learn.Web.Menus;

public class LearnMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        var administration = context.Menu.GetAdministration();
        var l = context.GetLocalizer<LearnResource>();

        context.Menu.Items.Insert(
            0,
            new ApplicationMenuItem(
                LearnMenus.Dashboard,
                l["Menu:Dashboard"],
                "~/dashboard",
                icon: "fas fa-home",
                order: 0
            ).RequirePermissions(LearnPermissions.Dashboard.Default)
        ); 

        context.Menu.Items.Insert(
            1,
            new ApplicationMenuItem(
                LearnMenus.Student,
                l["Menu:Students"],
                 "~/students",
                icon: "fa fa-user",
                order: 2
            ).RequirePermissions(LearnPermissions.Students.Default)
        );

        context.Menu.Items.Insert(
            2,
            new ApplicationMenuItem(
                LearnMenus.Question,
                l["Menu:Question"],
                 "~/questions",
                icon: "fa fa-copy"
            ).RequirePermissions(LearnPermissions.Question.Default)
        );

        context.Menu.Items.Insert(
            3,
            new ApplicationMenuItem(
                LearnMenus.Question,
                l["Menu:Exam"],
                 "~/Exams",
                icon: "fa fa-envelope"
            ).RequirePermissions(LearnPermissions.Exam.Default)
        );

        context.Menu.Items.Insert(
            4,
            new ApplicationMenuItem(
                LearnMenus.ExamResult,
                l["Menu:ExamResult"],
                 "~/ExamResults",
                icon: "fa fa-clipboard"
            ).RequirePermissions(LearnPermissions.ExamResult.Default)
        );
        context.Menu.Items.Insert(
            5,
            new ApplicationMenuItem(
                LearnMenus.ExamResult,
                l["Menu:LogOut"],
                 "~/Account/Logout",
                icon: "fa fa-share"
            )
        );
        context.Menu.Items.Insert(
            6,
            new ApplicationMenuItem(
                LearnMenus.Course,
                l["Menu:Course"],
                "~/Courses",
                icon: "fa fa-cloud",
                order: 1
            ).RequirePermissions(LearnPermissions.Courses.Default)
        );


        if (MultiTenancyConsts.IsEnabled)
        {
            administration.SetSubItemOrder(TenantManagementMenuNames.GroupName, 1);
        }
        else
        {
            administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
        }

        administration.SetSubItemOrder(IdentityMenuNames.GroupName, 2);
        administration.SetSubItemOrder(SettingManagementMenuNames.GroupName, 3);

        return Task.CompletedTask;
    }
}