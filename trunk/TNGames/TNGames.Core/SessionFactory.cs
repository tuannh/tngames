/*
* <Header>
* <VersioninSystemInformation>
* </VersioninSystemInformation>
* <LegalInformation>
* Copyright 2007 by Cateno Vietnam Ltd.
* 2/51 Phan Thuc Duyen, Tan Binh District, 
* Ho Chi Minh City, Vietnam
* All rights reserved.
*
* This software is the confidential and proprietary information
* of Cateno Vietnam and Cateno AS. ("Confidential Information"). You
* shall not disclose such Confidential Information and shall
* use it only in accordance with the terms of the license
* agreement you entered into with Cateno.
* </LegalInformation>
* </Header>
*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Web;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Mapping;
using Environment = System.Environment;


namespace TNGames.Core
{
    /// <summary>
    /// The SessionFactory provides the NHibernate sessions and provides the possibility to register
    /// additional classes with NHibernate by modules.
    /// </summary>
    public class SessionFactory
    {
        private const string SessionKey = "nhibernate.current_session";
        private static ISessionFactory sessionFactory;
        private const string NhibernateConfig = "nhibernate.config";
        /// <summary>
        /// Initializes the <see cref="SessionFactory"/> class.
        /// </summary>
        static SessionFactory()
        {
            //Setup the configuration
            NHibernate.Cfg.Configuration config;
            if (WebConfig.NHibernateConfigFile.Trim().Length > 0)
                config = new NHibernate.Cfg.Configuration().Configure(WebConfig.NHibernateConfigFile);
            else
                config = new NHibernate.Cfg.Configuration();

            try
            {
                //todo: Mrthanh 
                config.AddAssembly(Assembly.GetExecutingAssembly());

                // Build the session factory
                sessionFactory = config.BuildSessionFactory();

                // Add by Tuannh
                // Close current session
                CloseSession();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString());
            }
        }

        /// <summary>
        /// GetNHibernateFactory returns the current NHibernate ISessionFactory.
        /// </summary>
        public ISessionFactory GetNHibernateFactory()
        {
            return sessionFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ISession GetCurrentSession()
        {
            HttpContext context = HttpContext.Current;
            ISession currentSession = (ISession)context.Items[SessionKey];

            if (currentSession == null)
            {
                currentSession = sessionFactory.OpenSession();
                context.Items[SessionKey] = currentSession;
            }

            return currentSession;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CloseSession()
        {
            HttpContext context = HttpContext.Current;
            ISession currentSession = (ISession)context.Items[SessionKey];

            if (currentSession == null)
            {
                // No current session
                return;
            }
            currentSession.Close();
            context.Items.Remove(SessionKey);
        }

        /// <summary>
        /// 
        /// </summary>
        public static void CloseSessionFactory()
        {
            if (sessionFactory != null)
            {
                sessionFactory.Close();
            }
        }

        /// <summary>
        /// Gets the Session Key of NHibernate Session
        /// </summary>
        public string NHibernateSessionKey
        {
            get
            {
                return SessionKey;
            }
        }


        /// <summary>
        /// Gets the Session Key of NHibernate Session
        /// </summary>
        public string NHibernateConfigurationKey
        {
            get
            {
                return NhibernateConfig;
            }
        }

        /// <summary>
        /// Convert IList to IList<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="iList"></param>
        /// <returns></returns>
        public static IList<T> ConvertToListOf<T>(IList iList)
        {
            IList<T> result = new List<T>();
            foreach (T value in iList)
                result.Add(value);
            return result;
        }

    }
}
