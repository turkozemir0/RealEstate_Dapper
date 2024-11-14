using Dapper;
using RealEstate_Dapper_Api.DTOs.CategoryDTOs;
using RealEstate_Dapper_Api.Models.DapperContext;

namespace RealEstate_Dapper_Api.Repositories.CategoryRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Context _context;
        public CategoryRepository(Context context)
        {
            _context = context;
        }

        public async void CreateCategory(CreateCategoryDTO categoryDTO)
        {
            string query = "insert into Category(CategoryName,CategoryStatus) values (@categoryName,@categoryStatus)";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryName", categoryDTO.CategoryName);
            parameters.Add("@categoryStatus", true);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }

        public async void DeleteCategory(int id)
        {
            string query = "delete from Category Where CategoryID = @categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@categoryID",id);
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(query,parameters);
            }
        }

        public async Task<List<ResultCategoryDTO>> GetAllCategoryAsync()
        {
            string query = "Select * from Category";
            using (var connection = _context.CreateConnection())
            {
                var values = await connection.QueryAsync<ResultCategoryDTO>(query);
                return values.ToList();
            }
        }

        public async Task<GetByIDCategoryDTO> GetCategory(int id)
        {
            string query = "select * from Category Where CategoryID=@CategoryID";
            var parameters = new DynamicParameters();
            parameters.Add("@CategoryID", id);
            using (var connection = _context.CreateConnection())
            {
              var values =  await connection.QueryFirstOrDefaultAsync<GetByIDCategoryDTO>(query,parameters);
              return values;
            }
        }

        public async void UpdateCategory(UpdateCategoryDTO categoryDTO)
        {
            string query = "Update Category Set CategoryName=@categoryName,CategoryStatus=@categoryStatus where CategoryID = @categoryID";
            var parameters = new DynamicParameters();
            parameters.Add("categoryID", categoryDTO.CategoryID);
            parameters.Add("categoryName",categoryDTO.CategoryName);
            parameters.Add("categoryStatus",categoryDTO.CategoryStatus);
            using (var connection = _context.CreateConnection())
            {
              await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
