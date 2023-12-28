using Kidoo.Learn.Consts;
using Kidoo.Learn.Consts.Course;
using Kidoo.Learn.Consts.Section;
using Kidoo.Learn.Courses;
using Kidoo.Learn.CourseSections;
using Kidoo.Learn.CourseTopics;
using Kidoo.Learn.Files;
using Kidoo.Learn.Instructors;
using Kidoo.Learn.Questions; 
using Kidoo.Learn.Students;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore; 
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Kidoo.Learn.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class LearnDbContext :
    AbpDbContext<LearnDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    #region Entities from Kiddo
    public DbSet<Course> Courses { get; set; }
    public DbSet<CourseSection> CourseSections { get; set; }
    public DbSet<CourseTopic> CourseTopics { get; set; }
    
    public DbSet<Instructor> Instructors { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionOption> QuestionOptions { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<KidooFile> KidooFiles { get; set; }
    #endregion 
    

    public LearnDbContext(DbContextOptions<LearnDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

      

        builder.Entity<Instructor>(b =>
        {
            b.ToTable(LearnConsts.DbTablePrefix + "Instructors", LearnConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.FullName).HasMaxLength(InstructorConsts.MaxFullNameLength).IsRequired();
            b.Property(x => x.FirstName).HasMaxLength(InstructorConsts.MaxFirstNameLength).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(InstructorConsts.MaxLastNameLength).IsRequired();
            b.Property(x => x.Qualification).HasMaxLength(InstructorConsts.MaxQualificationLength);
        });

        builder.Entity<Question>(b =>
        {
            b.ToTable(LearnConsts.DbTablePrefix + "Questions", LearnConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Title).HasMaxLength(QuestionConsts.MaxTitleLength).IsRequired();
            b.Property(x => x.QuestionUniqueId).HasMaxLength(QuestionConsts.MaxQuestionUniqueIdLength);
            b.Property(x => x.Description).HasMaxLength(QuestionConsts.MaxDescriptionLength);
            b.Property(x => x.QuestionImageFile).HasMaxLength(QuestionConsts.MaxQuestionImageFileLength);
            b.Property(x => x.CorrectAnswer).HasMaxLength(QuestionConsts.MaxCorrectAnswerLength).IsRequired(false);
            b.Property(x => x.StoryGroup).IsRequired(false);

        });

        builder.Entity<QuestionOption>(b =>
        {
            b.ToTable(LearnConsts.DbTablePrefix + "QuestionOptions", LearnConsts.DbSchema);
            b.ConfigureByConvention(); 
            b.Property(x => x.OptionText).HasMaxLength(QuestionOptionConsts.MaxOptionTextLength).IsRequired();

        });

      

        builder.Entity<Student>(b =>
        {
            b.ToTable(LearnConsts.DbTablePrefix + "Students", LearnConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.FirstName).HasMaxLength(StudentConsts.MaxFullNameLength).IsRequired();
            b.Property(x => x.FirstName).HasMaxLength(StudentConsts.MaxFirstNameLength).IsRequired();
            b.Property(x => x.LastName).HasMaxLength(StudentConsts.MaxLastNameLength).IsRequired();
            b.Property(x => x.GuardianName).HasMaxLength(StudentConsts.MaxGuardianNameLength).IsRequired();
            b.Property(x => x.Gender).HasMaxLength(StudentConsts.MaxGenderLength).IsRequired();
            b.Property(x => x.Address).HasMaxLength(StudentConsts.MaxGenderLength).IsRequired();
        });


        builder.Entity<Course>(b =>
        {
            b.ToTable(LearnConsts.DbTablePrefix + "Courses", LearnConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Title).HasMaxLength(CourseConsts.MaxTitleLength).IsRequired();
            b.Property(x => x.Description).HasMaxLength(CourseConsts.MaxDescriptionLength).IsRequired();
            b.Property(x => x.ThumbnailUrl).IsRequired();
        });

        builder.Entity<CourseSection>(b =>
        {
            b.ToTable(LearnConsts.DbTablePrefix + "CourseSections", LearnConsts.DbSchema);
            b.ConfigureByConvention();

            b.Property(x => x.Title).HasMaxLength(CourseSectionConsts.MaxTitleLength).IsRequired();
            b.Property(x => x.ThumbnailUrl).IsRequired();
        });

        builder.Entity<CourseTopic>(b =>
        {
            b.ToTable(LearnConsts.DbTablePrefix + "CourseTopics", LearnConsts.DbSchema);
            b.ConfigureByConvention();
        });        
        
        builder.Entity<KidooFile>(b =>
        {
            b.ToTable(LearnConsts.DbTablePrefix + "Files", LearnConsts.DbSchema);
            b.ConfigureByConvention();
        });
    }
}
