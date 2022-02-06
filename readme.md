# dsn

### migrations

```bash
cd src
# restore dotnet tool, run once only
dotnet tool restore

# check database
dotnet ef dbcontext info -s pos.web

# apply migrations
dotnet ef database update -s pos.web

# add new migration
dotnet ef migrations add Init -s pos.web/ -p core/dsn.data/

# generate a migration script
dotnet ef migrations script -s src/pos.web/ -p src/pos.data/ -o src/pos.data/Migrations/sql/1-init.sql
```
