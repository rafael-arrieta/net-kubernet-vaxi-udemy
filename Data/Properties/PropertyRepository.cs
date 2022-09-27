using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetKubernet.Middleware;
using NetKubernet.Models;
using NetKubernet.Token;

namespace NetKubernet.Data.Properties;
public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;
    private readonly IUserSession _userSession;
    private readonly UserManager<User> _userManager;
    public PropertyRepository(
        AppDbContext context,
        IUserSession session,
        UserManager<User> userManager
    )
    {
        _context = context;
        _userSession = session;
        _userManager = userManager;
    }
    public async Task CreateProperty(Property property)
    {
        var User = await _userManager.FindByNameAsync(_userSession.GetUserSession());

        if(User is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.Unauthorized,
                new {message = "the user is invalid to do this insertion"}
            );
        }
        if(property is null)
        {
            throw new MiddlewareException(
                HttpStatusCode.BadRequest,
                new {message = "the property's data are incorrect"}
            );
        }
        property.DateCreated = DateTime.Now;
        property.UserId = Guid.Parse(User!.Id);

        await _context.Properties!.AddAsync(property);
    }
    public async Task DeleteProperty(int id)
    {
        var property = await _context.Properties!
                            .FirstOrDefaultAsync(x => x.Id == id);

        _context.Properties!.Remove(property!);

    }
    public async Task <IEnumerable<Property>> GetAllProperties()
    {
        return await _context.Properties!.ToListAsync();
    }
    public async Task<Property> GetPropertyById(int id)
    {
        return (await _context.Properties!.FirstOrDefaultAsync(x => x.Id == id))!;
    }
    public async Task<bool> SaveChanges()
    {
        return ((await _context.SaveChangesAsync()) >= 0);
    }

}

