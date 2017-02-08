using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Subdivisionary.Models;
using Subdivisionary.Models.Applications;
using Subdivisionary.Models.Collections;
using Subdivisionary.Models.Forms;
using Subdivisionary.Models.ProjectInfos;

namespace Subdivisionary.DAL
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public Applicant Data { get; set; }
        public int DataId { get; set; }
        public string Name { get; set; }

        public ApplicationUser()
        {
            
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Application> Applications { get; set; }
        public DbSet<Applicant> Applicants { get; set; }
        public DbSet<BasicProjectInfo> ProjectInfos { get; set; }
        public DbSet<Form> Forms { get; set; }
        //public DbSet<FileUploadInfo> FileUploads { get; set; }

        public bool ProxyEnabled {
            get
            {
                return this.Configuration.ProxyCreationEnabled;
            }
            set
            {
                this.Configuration.ProxyCreationEnabled = value;
            }
        }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        /**
         * Create a model with complex relationships & identification maps
         */
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // The plural convention causes a table to be named "Dbo.Applicant"
            // instead of "Dbo.Applicants", while using code-first migratrations.
            // so I removed it
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Map many applicant to many applications
            modelBuilder.Entity<Applicant>()
                .HasMany(a => a.Applications)
                .WithMany(app => app.Applicants)
                .Map(cs =>
                    {
                        cs.MapLeftKey("ApplicantRefId");
                        cs.MapRightKey("ApplicationRefId");
                        cs.ToTable("ApplicantsToApplications");
                    }
                );

            // Map one application to many forms
            modelBuilder.Entity<Application>()
                .HasMany(app => app.Forms)
                .WithRequired(f => f.Application);

            // map the two-way relationship between ProjectInfos & Applications where both are required
            modelBuilder.Entity<BasicProjectInfo>()
                .HasRequired(info => info.Application) // issue requirement
                .WithRequiredDependent(proj => proj.ProjectInfo) // application class in charge
                .WillCascadeOnDelete(true); // if one gets deleted so does the other
            
            // map the two-way relationship between ApplicationUser & Applicant where both are required
            modelBuilder.Entity<ApplicationUser>()
                .HasRequired(user => user.Data)
                .WithRequiredPrincipal(data => data.User);
            
            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        sb.AppendLine(string.Format("Property: {0} Error: {1}",
                                                validationError.PropertyName,
                                                validationError.ErrorMessage));
                    }
                }
                var error = sb.ToString();
                System.Diagnostics.Debug.WriteLine(error);
                Console.WriteLine(error);
                throw dbEx;
            }
        }
    }
}