using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography.X509Certificates;
using WebAPI_Practice.Model;

namespace WebAPI_Practice.Data
{
    public class CountryRepository
    {
        #region configuration
        private readonly string _connectionString;
        public CountryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConnectionString");
        }
        #endregion

        #region SelectAllCountries
        public IEnumerable<CountryModel> SelectAllCountries()
        {
            var countries = new List<CountryModel>();
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_Country_SelectAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countries.Add(new CountryModel()
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString()

                    });
                }
            }
            return countries;
        }
        #endregion

        #region InsertCountry
        public bool InsertCountry(CountryModel country)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_Country_Insert", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                //cmd.Parameters.AddWithValue("@CountryID", country.CountryID);
                cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
                cmd.Parameters.AddWithValue("@CountryCode", country.CountryCode);
                cmd.Parameters.AddWithValue("@CreatedDate", DateTime.Now);


                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;

            }
        }
        #endregion

        #region UpdateCountry
        public bool UpdateCountry(CountryModel country)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_Country_Update", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CountryID", country.CountryID);
                cmd.Parameters.AddWithValue("@CountryName", country.CountryName);
                cmd.Parameters.AddWithValue("@CountryCode", country.CountryCode);
                cmd.Parameters.AddWithValue("@ModifiedDate", SqlDbType.DateTime).Value = DBNull.Value;

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;

            }
        }
        #endregion

        #region DeleteCountry
        public bool DeleteCountry(int countryId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_Country_Delete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CountryID", countryId);

                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0; // Returns true if at least one row is affected, indicating a successful delete
            }
        }
        #endregion

        #region GetCountryByID
        public IEnumerable<CountryModel> GetByCountryID(int countryID)

        {

            var countryByID = new List<CountryModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = new SqlCommand("PR_LOC_Country_SelectByPK", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    countryByID.Add(new CountryModel
                    {
                        CountryID = Convert.ToInt32(reader["CountryID"]),
                        CountryName = reader["CountryName"].ToString(),
                        CountryCode = reader["CountryCode"].ToString()
                    });
                }
            }
            return countryByID;
        }
        #endregion

    }
}