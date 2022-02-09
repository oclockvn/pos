# pos

### migrations

```bash
cd src
# restore dotnet tool, run once only
dotnet tool restore

# check database
dotnet ef dbcontext info -s src/pos.web

# apply migrations
dotnet ef database update -s src/pos.web

# add new migration
dotnet ef migrations add Init -s src/pos.web/ -p src/pos.core/

# generate a migration script
dotnet ef migrations script -s src/pos.web/ -p src/pos.core/ -o src/pos.core/Migrations/sql/1-init.sql
```
