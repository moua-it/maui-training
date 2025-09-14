using ContactSyncApp.Interface;

namespace ContactSyncApp.Dal;

public class ContactRepository(ContactSyncDatabase<Model.Contact> database) : IRepository<Model.Contact>
{
    public async Task<List<Model.Contact>> GetAllAsync()
    {
        return await database.GetAllAsync();
    }

    public async Task<Model.Contact> GetByIdAsync(int id)
    {
        return await database.GetByIdAsync(id);
    }

    public async Task AddAsync(Model.Contact contact)
    {
        await database.AddAsync(contact);
    }
    public async Task UpdateAsync(Model.Contact contact)
    {
        await database.UpdateAsync(contact);
    }
    public async Task DeleteAsync(int id)
    {
        var contact = await database.GetByIdAsync(id);

        if (contact != null)
            await database.DeleteAsync(contact);
    }
}

