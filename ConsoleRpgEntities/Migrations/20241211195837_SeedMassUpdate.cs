using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRpgEntities.Migrations
{
    public partial class SeedMassUpgrade : BaseMigration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            RunSql(migrationBuilder);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            RunSqlRollback(migrationBuilder);
        }
    }
}
