using OrderService.Infrastructure;
using OrderService.Infrastructure.Database;

public class DbInitialiser
{
    private readonly OrderServiceContext _context;

    public DbInitialiser(OrderServiceContext context)
    {
        _context = context;
    }

    public void Run()
    {
        // TODO: Add initialisation logic.
    }
}