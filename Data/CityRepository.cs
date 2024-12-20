using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WebAPI_Practice.Model;
using static WebAPI_Practice.Controllers.CityController;

namespace WebAPI_Practice.Data
{
    public class CityRepository
    {
        #region Private Field for Configuration
        private readonly string _connectionString;
        #endregion

        #region Constructor
        public CityRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAllCities
        public IEnumerable<CityModel> SelectAllCities()
        {
            var cities = new List<CityModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_SelectAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new CityModel()
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CountryName = reader["CountryName"].ToString(),
                        StateName = reader["StateName"].ToString(),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString()
                    });
                }
            }
            return cities;
        }
        #endregion

        #region GetCityByID
        public IEnumerable<CityModel> GetCityByID(int cityID)
        {
            var cityByID = new List<CityModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CityID", cityID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cityByID.Add(new CityModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CityName = reader["CityName"].ToString(),
                        CityCode = reader["CityCode"].ToString(),
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                    });
                }
            }
            return cityByID;
        }
        #endregion

        #region Insertcity  
        public bool Insertcity(CityModel city)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CityName", city.CityName);
                cmd.Parameters.AddWithValue("@CityCode", city.CityCode);
                cmd.Parameters.AddWithValue("@StateID", city.StateID);
                cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        #endregion

        #region UpdateCity
        public bool UpdateCity(CityModel city)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CityID", city.CityID);
                cmd.Parameters.AddWithValue("@CityName", city.CityName);
                cmd.Parameters.AddWithValue("@CityCode", city.CityCode);
                cmd.Parameters.AddWithValue("@StateID", city.StateID);
                cmd.Parameters.AddWithValue("@CountryID", city.CountryID);
                cmd.Parameters.AddWithValue("@ModifiedDate", SqlDbType.DateTime).Value = DBNull.Value;

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();   
                return rowsAffected > 0;

            }
        }
        #endregion

        #region Deletecity
        [HttpDelete("{cityID}")]
        public bool DeleteCity(int cityID)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_City_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@cityID", cityID);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0; // Returns true if at least one row is affected, indicating a successful delete
            }
        }
        #endregion

       
    }
}
