namespace CleanArchitecture.Infraestructure.Repositories;

public class BaseRepository<T>(StreamerDbContext context) : IAsyncRepository<T> where T : BaseDomainModel
{
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
        return await context.Set<T>().Where(predicate).ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = context.Set<T>();
        
        if(disableTracking)
            query = query.AsNoTracking();
        
        if(includeString != null)
            query = query.Include(includeString);
        
        if(predicate != null)
            query = query.Where(predicate);
        
        if(orderBy != null)
            query = orderBy(query);
        
        return await query.ToListAsync();
    }

    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, 
        Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
        List<Expression<Func<T, object>>>? includeExpressions = null,
        bool disableTracking = true)
    {
        IQueryable<T> query = context.Set<T>();
        
        if(disableTracking)
            query = query.AsNoTracking();
        
        if(includeExpressions != null)
            query = includeExpressions.Aggregate(
                query, (current, includeExpression) => current.Include(includeExpression));
        
        if(predicate != null)
            query = query.Where(predicate);
        
        if(orderBy != null)
            query = orderBy(query);
        
        return await query.ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }

    public async Task<T> AddAsync(T entity)
    {
        await context.Set<T>().AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(T entity)
    {
        context.Set<T>().Remove(entity);
        await context.SaveChangesAsync();
    }
}