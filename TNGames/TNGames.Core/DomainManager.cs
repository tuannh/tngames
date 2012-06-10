using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using NHibernate;
using NHibernate.Criterion;
using TNGames.Core;

namespace TNGames.Core
{
    public class NameValuePair
    {
        private string name;
        private object value;

        public NameValuePair(string name, object value)
        {
            this.name = name;
            this.value = value;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public object Value
        {
            get { return value; }
            set { this.value = value; }
        }
    }

    public static class DomainManager
    {
        #region Properties

        public static ISession Session
        {
            get { return SessionFactory.GetCurrentSession(); }
        }

        #endregion

        #region Manage exception support methods

        private static void ManageException(Exception exp)
        {
#if DEBUG
            // do something here to manage exception
            throw exp;
#endif
        }

        #endregion

        #region GetObject<T>

        /// <summary>
        /// Get an object by id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetObject<T>(object id)
        {
            return Session.Get<T>(id);
        }

        #endregion

        #region GetAll<T>

        /// <summary>
        /// Get all T object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortOrder">Order type: Descending or Ascending</param>
        /// <param name="sortPropertyNames">A string array of properties will be sort</param>
        /// <returns></returns>
        public static IList<T> GetAll<T>(SortOrderType sortOrder, params string[] sortPropertyNames)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            if (sortPropertyNames != null)
            {
                if (SortOrderType.Ascending == sortOrder)
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Asc(propertyName));
                }
                else
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Desc(propertyName));

                }
            }
            return criteria.List<T>();
        }

        /// <summary>
        /// Get all T object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortOrder">Order type: Descending or Ascending</param>
        /// <param name="sortPropertyNames">A string array of properties will be sort</param>
        /// <returns></returns>
        public static IList<T> GetAll<T>(SortOrderType sortOrder, ArrayList sortPropertyNames)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            if (sortPropertyNames != null)
            {
                if (SortOrderType.Ascending == sortOrder)
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Asc(propertyName));
                }
                else
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Desc(propertyName));

                }
            }
            return criteria.List<T>();
        }

        /// <summary>
        /// Get all T objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IList<T> GetAll<T>()
        {
            return GetAll<T>(SortOrderType.Ascending, new ArrayList());
        }

        /// <summary>
        /// Get all T objects
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="sortOrder">Sort order in result set</param>
        /// <param name="sortPropertyNames">Names of properites need to order</param>
        /// <returns></returns>
        public static IList<T> GetAll<T>(int pageIndex, int pageSize, out int totalRows, SortOrderType sortOrder, params string[] sortPropertyNames)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            if (sortPropertyNames != null)
            {
                if (SortOrderType.Ascending == sortOrder)
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Asc(propertyName));
                }
                else
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Desc(propertyName));

                }
            }

            totalRows = criteria.List<T>().Count;
            criteria.SetFirstResult((pageIndex - 1) * pageSize);
            criteria.SetMaxResults(pageSize);

            return criteria.List<T>();
        }

        /// <summary>
        /// Get all T object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <returns></returns>
        public static IList<T> GetAll<T>(int pageIndex, int pageSize, out int totalRows)
        {
            return GetAll<T>(pageIndex, pageSize, out totalRows, SortOrderType.Ascending, null);
        }

        #endregion

        #region GetList<T>

        /// <summary>
        /// Get a list of T by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortPropertie">Name of sort properties</param>
        /// <param name="sortOrder">Sort Order: Asc or Desc</param>
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList<T> GetList<T>(SortOrderType sortOrder, string sortPropertyName, params ICriterion[] criterions)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            if (criterions != null)
            {
                foreach (ICriterion expr in criterions)
                {
                    criteria.Add(expr);
                }
            }
            if (!string.IsNullOrEmpty(sortPropertyName))
            {
                if (SortOrderType.Ascending == sortOrder)
                    criteria.AddOrder(Order.Asc(sortPropertyName));
                else
                    criteria.AddOrder(Order.Desc(sortPropertyName));
            }

            return criteria.List<T>();
        }

        /// <summary>
        /// Get a list of T by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortOrder">Sort Order type: Ascedning or Descending</param>
        /// <param name="sortPropertie">Name of sort properties</param>        
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList<T> GetList<T>(SortOrderType sortOrder, string sortPropertyName, ArrayList criterions)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            if (criterions != null)
            {
                foreach (ICriterion expr in criterions)
                {
                    criteria.Add(expr);
                }
            }
            if (!string.IsNullOrEmpty(sortPropertyName))
            {
                if (SortOrderType.Ascending == sortOrder)
                    criteria.AddOrder(Order.Asc(sortPropertyName));
                else
                    criteria.AddOrder(Order.Desc(sortPropertyName));
            }

            return criteria.List<T>();
        }

        /// <summary>
        /// Get a list of T by criterion expressions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList<T> GetList<T>(params ICriterion[] criterions)
        {
            return GetList<T>(SortOrderType.Ascending, null, criterions);
        }

        /// <summary>
        /// Get a list of T by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="sortOrder">Sort Order: Ascending or Descending</param>
        /// <param name="sortPropertie">Name of sort properties</param>        
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList<T> GetList<T>(int pageIndex, int pageSize, out int totalRows, SortOrderType sortOrder, string sortPropertyName, params ICriterion[] criterions)
        {
            ICriteria criteria = Session.CreateCriteria(typeof(T));
            if (criterions != null)
            {
                foreach (ICriterion expr in criterions)
                {
                    criteria.Add(expr);
                }
            }
            if (!string.IsNullOrEmpty(sortPropertyName))
            {
                if (SortOrderType.Ascending == sortOrder)
                    criteria.AddOrder(Order.Asc(sortPropertyName));
                else
                    criteria.AddOrder(Order.Desc(sortPropertyName));
            }

            totalRows = criteria.List<T>().Count;
            criteria.SetFirstResult((pageIndex - 1) * pageSize);
            criteria.SetMaxResults(pageSize);

            return criteria.List<T>();
        }

        /// <summary>
        /// Get a list of T by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList<T> GetList<T>(int pageIndex, int pageSize, out int totalRows, params ICriterion[] criterions)
        {
            return GetList<T>(pageIndex, pageSize, out totalRows, SortOrderType.Ascending, null, criterions);
        }

        /// <summary>
        /// Get T list from NHibernate query string
        /// </summary>
        /// <param name="hquery">Nhibernate query string</param>
        /// <param name="nvPairs">a name value pair list</param>
        /// <returns>A T result list</returns>
        public static IList<T> GetList<T>(string hquery, params NameValuePair[] nvPairs)
        {
            IQuery query = Session.CreateQuery(hquery);
            if (nvPairs != null)
            {
                foreach (NameValuePair nvp in nvPairs)
                    query.SetParameter(nvp.Name, nvp.Value);
            }


            return query.List<T>();
        }

        /// <summary>
        /// Get T list from NHibernate query string
        /// </summary>
        /// <param name="hquery">NHibernate query string</param>
        /// <param name="nvPairs">a name value pair list</param>
        /// <returns>A T result list</returns>
        public static IList<T> GetList<T>(string hquery, ArrayList nvPairs)
        {
            IQuery query = Session.CreateQuery(hquery);
            if (nvPairs != null)
            {
                foreach (NameValuePair nvp in nvPairs)
                    query.SetParameter(nvp.Name, nvp.Value);
            }

            return query.List<T>();
        }

        /// <summary>
        /// Get T list from NHibernate query string
        /// </summary>
        /// <param name="hquery">NHibernate query string</param>
        /// <returns>A T result list</returns>
        public static IList<T> GetList<T>(string hquery)
        {
            return GetList<T>(hquery, new ArrayList());
        }

        /// <summary>
        /// Get T list from NHibernate query string
        /// </summary>
        /// <param name="hquery">Nhibernate query string</param>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="nvPairs">a name value pair list</param>
        /// <returns>A T result list</returns>
        public static IList<T> GetList<T>(string hquery, int pageIndex, int pageSize, out int totalRows, params NameValuePair[] nvPairs)
        {
            IQuery query = Session.CreateQuery(hquery);
            if (nvPairs != null)
            {
                foreach (NameValuePair nvp in nvPairs)
                    query.SetParameter(nvp.Name, nvp.Value);
            }

            totalRows = query.List<T>().Count;
            query.SetFirstResult((pageIndex - 1) * pageSize);
            query.SetMaxResults(pageSize);

            return query.List<T>();
        }

        /// <summary>
        /// Get T list from NHibernate query string
        /// </summary>
        /// <param name="hquery">Nhibernate query string</param>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="nvPairs">a name value pair list</param>
        /// <returns>A T result list</returns>
        public static IList<T> GetList<T>(string hquery, int pageIndex, int pageSize, out int totalRows)
        {
            return GetList<T>(hquery, pageIndex, pageSize, out totalRows, null);
        }

        #endregion

        #region GetObject

        /// <summary>
        /// Get an object by id
        /// </summary>        
        public static object GetObject(Type type, object id)
        {
            return Session.Get(type, id);
        }

        #endregion

        #region GetAll

        /// <summary>
        /// Get all T objects by object type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="sortOrder">Sort Order: Ascending or Descending</param>
        /// <param name="sortPropertie">Name of sort properties</param>        
        /// <returns></returns>      
        public static IList GetAll(Type type, SortOrderType sortOrder, params string[] sortPropertyNames)
        {
            ICriteria criteria = Session.CreateCriteria(type);
            if (sortPropertyNames != null)
            {
                if (SortOrderType.Ascending == sortOrder)
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Asc(propertyName));
                }
                else
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Desc(propertyName));

                }
            }
            return criteria.List();
        }

        /// <summary>
        /// Get all T objects by object type
        /// </summary>       
        public static IList GetAll(Type type)
        {
            return GetAll(type, SortOrderType.Ascending, null);
        }

        /// <summary>
        /// Get all T objects by object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="sortOrder">Sort Order: Ascending or Descending</param>
        /// <param name="sortPropertie">Name of sort properties</param>        
        /// <returns></returns>
        public static IList GetAll(Type type, int pageIndex, int pageSize, out int totalRows, SortOrderType sortOrder, params string[] sortPropertyNames)
        {
            ICriteria criteria = Session.CreateCriteria(type);
            if (sortPropertyNames != null)
            {
                if (SortOrderType.Ascending == sortOrder)
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Asc(propertyName));
                }
                else
                {
                    foreach (string propertyName in sortPropertyNames)
                        criteria.AddOrder(Order.Desc(propertyName));

                }
            }

            totalRows = criteria.List().Count;
            criteria.SetFirstResult((pageIndex - 1) * pageSize);
            criteria.SetMaxResults(pageSize);


            return criteria.List();
        }

        /// <summary>
        /// Get all T objects by object type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <returns></returns>
        public static IList GetAll(Type type, int pageIndex, int pageSize, out int totalRows)
        {
            return GetAll(type, pageIndex, pageSize, out totalRows, SortOrderType.Ascending, null);
        }
        #endregion

        #region GetList

        /// <summary>
        /// Get a list of type object by criterion expressions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList GetList(Type type, params ICriterion[] criterions)
        {
            return GetList(type, SortOrderType.Ascending, null, criterions);
        }

        /// <summary>
        /// Get a list of type object by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortOrder">Sort Order: Ascending or Descending</param>
        /// <param name="sortPropertie">Name of sort properties</param>        
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList GetList(Type type, SortOrderType sortOrder, string sortProperty, params ICriterion[] criterions)
        {
            ICriteria criteria = Session.CreateCriteria(type);
            if (criterions != null)
            {
                foreach (ICriterion expr in criterions)
                {
                    criteria.Add(expr);
                }
            }
            if (!string.IsNullOrEmpty(sortProperty))
            {
                if (SortOrderType.Ascending == sortOrder)
                    criteria.AddOrder(Order.Asc(sortProperty));
                else
                    criteria.AddOrder(Order.Desc(sortProperty));
            }

            return criteria.List();
        }

        /// <summary>
        /// Get a list of type object by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sortOrder">Sort Order: Ascending or Descending</param>
        /// <param name="sortPropertie">Name of sort properties</param>        
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList GetList(Type type, SortOrderType sortOrder, string sortProperty, ArrayList criterions)
        {
            ICriteria criteria = Session.CreateCriteria(type);
            if (criterions != null)
            {
                foreach (ICriterion expr in criterions)
                {
                    criteria.Add(expr);
                }
            }
            if (!string.IsNullOrEmpty(sortProperty))
            {
                if (SortOrderType.Ascending == sortOrder)
                    criteria.AddOrder(Order.Asc(sortProperty));
                else
                    criteria.AddOrder(Order.Desc(sortProperty));
            }

            return criteria.List();
        }

        /// <summary>
        /// Get a list of type object by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="sortOrder">Sort Order: Ascending or Descending</param>
        /// <param name="sortPropertie">Name of sort properties</param>               
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList GetList(Type type, int pageIndex, int pageSize, out int totalRows, SortOrderType sortOrder, string sortProperty, params ICriterion[] criterions)
        {
            ICriteria criteria = Session.CreateCriteria(type);
            if (criterions != null)
            {
                foreach (ICriterion expr in criterions)
                {
                    criteria.Add(expr);
                }
            }
            if (!string.IsNullOrEmpty(sortProperty))
            {
                if (SortOrderType.Ascending == sortOrder)
                    criteria.AddOrder(Order.Asc(sortProperty));
                else
                    criteria.AddOrder(Order.Desc(sortProperty));
            }

            totalRows = criteria.List().Count;
            criteria.SetFirstResult((pageIndex - 1) * pageSize);
            criteria.SetMaxResults(pageSize);

            return criteria.List();
        }

        /// <summary>
        /// Get a list of type object by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="criterions">A list of criterion expressions</param>
        /// <returns></returns>
        public static IList GetList(Type type, int pageIndex, int pageSize, out int totalRows, params ICriterion[] criterions)
        {
            return GetList(type, pageIndex, pageSize, out totalRows, SortOrderType.Ascending, null, criterions);
        }

        /// <summary>
        /// Get a list of type object by criterion expression
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <returns></returns>
        public static IList GetList(Type type, int pageIndex, int pageSize, out int totalRows)
        {
            return GetList(type, pageIndex, pageSize, out totalRows, null);
        }

        /// <summary>
        /// Get list from NHibernate query string
        /// </summary>
        /// <param name="hquery">NHibernate query string</param>
        /// <param name="nvPairs">a name value pair list</param>
        /// <returns>A T result list</returns>
        public static IList GetList(string hquery, params NameValuePair[] nvPairs)
        {
            IQuery query = Session.CreateQuery(hquery);
            if (nvPairs != null)
            {
                foreach (NameValuePair nvp in nvPairs)
                    query.SetParameter(nvp.Name, nvp.Value);
            }

            return query.List();
        }

        /// <summary>
        /// Get list from NHibernate query string
        /// </summary>
        /// <param name="hquery">Nhibernate query string</param>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <param name="nvPairs">a name value pair list</param>
        /// <returns>A T result list</returns>
        public static IList GetList(string hquery, int pageIndex, int pageSize, out int totalRows, params NameValuePair[] nvPairs)
        {
            IQuery query = Session.CreateQuery(hquery);
            if (nvPairs != null)
            {
                foreach (NameValuePair nvp in nvPairs)
                    query.SetParameter(nvp.Name, nvp.Value);
            }

            totalRows = query.List().Count;
            query.SetFirstResult((pageIndex - 1) * pageSize);
            query.SetMaxResults(pageSize);

            return query.List();
        }

        /// <summary>
        /// Get list from NHibernate query string
        /// </summary>
        /// <param name="hquery">Nhibernate query string</param>
        /// <param name="pageIndex">Index of page need to get data. Start with: 1, 2, 3,....</param>
        /// <param name="pageSize">Max record return</param>
        /// <param name="totalRows">Total record in datasource</param>
        /// <returns>A T result list</returns>
        public static IList GetList(string hquery, int pageIndex, int pageSize, out int totalRows)
        {
            return GetList(hquery, pageIndex, pageSize, out totalRows, null);
        }

        /// <summary>
        /// Get list from NHibernate query string
        /// </summary>
        /// <param name="hquery">NHibernate query string</param>
        /// <param name="nvPairs">an array contains name value pair list</param>
        /// <returns>A T result list</returns>
        public static IList GetList(string hquery, ArrayList nvPairs)
        {
            IQuery query = Session.CreateQuery(hquery);
            if (nvPairs != null)
            {
                foreach (NameValuePair nvp in nvPairs)
                    query.SetParameter(nvp.Name, nvp.Value);
            }

            return query.List();
        }

        /// <summary>
        /// Get T list from NHibernate query string
        /// </summary>
        /// <param name="hquery">NHibernate query string</param>
        /// <returns>A T result list</returns>
        public static IList GetList(string hquery)
        {
            return GetList(hquery, new ArrayList());
        }

        #endregion

        #region Save/Update/Delete

        /// <summary>
        /// Insert an new object to database
        /// </summary>
        /// <param name="obj">object needs to insert</param>
        /// <returns>true if save succefully other way return false</returns>
        public static bool Insert(object obj)
        {
            ITransaction tran = null;
            try
            {
                tran = Session.BeginTransaction();
                Session.Save(obj);
                tran.Commit();
                return true;
            }
            catch (Exception exp)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                ManageException(exp);
            }
            return false;
        }

        /// <summary>
        /// Update an existing object in database
        /// </summary>
        /// <param name="obj">object needs to update</param>
        /// <returns>true if update successfully other way return false</returns>
        public static bool Update(object obj)
        {
            ITransaction tran = null;
            try
            {
                tran = Session.BeginTransaction();
                Session.Update(obj);
                tran.Commit();
                return true;
            }
            catch (Exception exp)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                ManageException(exp);
            }
            return false;
        }

        /// <summary>
        /// Insert or update an object
        /// </summary>
        /// <param name="obj">object needs to save or update</param>
        /// <returns>true if update successfully other way return false</returns>
        public static bool InsertOrUpdate(object obj)
        {
            ITransaction tran = null;
            try
            {
                tran = Session.BeginTransaction();
                Session.SaveOrUpdate(obj);
                tran.Commit();
                return true;
            }
            catch (Exception exp)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                ManageException(exp);
            }
            return false;
        }

        /// <summary>
        /// Delete a existing object from database
        /// </summary>
        /// <param name="obj">object need to delete</param>
        /// <returns>true if update successfully other way return false</returns>
        public static bool Delete(object obj)
        {
            ITransaction tran = null;
            try
            {
                tran = Session.BeginTransaction();
                Session.Delete(obj);
                tran.Commit();
                return true;
            }
            catch (Exception exp)
            {
                if (tran != null)
                {
                    tran.Rollback();
                }
                ManageException(exp);
            }
            return false;
        }

        /// <summary>
        /// Delete a object by object type and id
        /// </summary>
        /// <param name="type">object type need to delete</param>
        /// <param name="id">id of object need to delete</param>
        /// <returns>true if update successfully other way return false</returns>
        public static bool Delete(Type type, object id)
        {
            object obj = GetObject(type, id);
            if (obj != null)
                return Delete(obj);

            return false;
        }

        /// <summary>
        /// Flush the changes from the Session to the Database
        /// </summary>
        public static void Flush()
        {
            try
            {
                Session.Flush();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region execute sql query support methods

        /// <summary>
        /// Execute a query string
        /// </summary>
        /// <param name="query">query string or store procedure string</param>
        /// <param name="cmdType">cmdType is Text or StoredProcedure</param>
        /// <param name="paraList">a list of parameters</param>
        /// <returns>A datatable contains result data</returns>
        public static DataTable ExecuteQuery(string query, CommandType cmdType, params SqlParameter[] paraList)
        {
            try
            {
                IDbCommand cmd = Session.Connection.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = cmdType;
                if (paraList != null)
                {
                    foreach (SqlParameter para in paraList)
                        cmd.Parameters.Add(para);
                }

                IDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception exp)
            {
                ManageException(exp);
                return null;
            }
        }

        /// <summary>
        /// Execute a query string
        /// </summary>
        /// <param name="query">query string or store procedure string</param>
        /// <param name="cmdType">cmdType is Text or StoredProcedure</param>
        /// <returns>A datatable contains result data</returns>
        public static DataTable ExecuteQuery(string query, CommandType cmdType)
        {
            return ExecuteQuery(query, cmdType, null);
        }

        /// <summary>
        /// Execute Scalar
        /// </summary>
        /// <param name="query">query string or store procedure string</param>
        /// <param name="cmdType">cmdType is Text or StoredProcedure</param>
        /// <param name="paraList">a list of parameters</param>
        /// <returns>a result object</returns>
        public static object ExecuteScalar(string query, CommandType cmdType, params SqlParameter[] paraList)
        {
            try
            {
                IDbCommand cmd = Session.Connection.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = cmdType;
                if (paraList != null)
                {
                    foreach (SqlParameter para in paraList)
                        cmd.Parameters.Add(para);
                }

                return cmd.ExecuteScalar();
            }
            catch (Exception exp)
            {
                ManageException(exp);
                return null;
            }
        }

        /// <summary>
        /// Execute Scalar
        /// </summary>
        /// <param name="query">query string or store procedure string</param>
        /// <param name="cmdType">cmdType is Text or StoredProcedure</param>
        /// <returns>an object</returns>
        public static object ExecuteScalar(string query, CommandType cmdType)
        {
            return ExecuteScalar(query, cmdType, null);
        }

        /// <summary>
        /// Execute non query string
        /// </summary>
        /// <param name="query">query string or store procedure string</param>
        /// <param name="cmdType">cmdType is Text or StoredProcedure</param>
        /// <param name="paraList">a list of parameters</param>
        public static void ExecuteNonQuery(string query, CommandType cmdType, params SqlParameter[] paraList)
        {
            try
            {
                IDbCommand cmd = Session.Connection.CreateCommand();
                cmd.CommandText = query;
                cmd.CommandType = cmdType;
                if (paraList != null)
                {
                    foreach (SqlParameter para in paraList)
                        cmd.Parameters.Add(para);
                }

                cmd.ExecuteNonQuery();

            }
            catch (Exception exp)
            {
                ManageException(exp);
            }
        }

        /// <summary>
        /// Execute non query string
        /// </summary>
        /// <param name="query">query string or store procedure string</param>
        /// <param name="cmdType">cmdType is Text or StoredProcedure</param>                
        public static void ExecuteNonQuery(string query, CommandType cmdType)
        {
            ExecuteNonQuery(query, cmdType, null);
        }

        #endregion
    }
}