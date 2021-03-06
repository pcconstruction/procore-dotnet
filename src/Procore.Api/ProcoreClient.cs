﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Procore.Api.Core.CompanyDirectory;
using Procore.Api.Core.Project;

[assembly: InternalsVisibleTo("Procore.Api.Tests")]
namespace Procore.Api
{
    /// <summary>
    ///     Represents a client that encapsulate other clients which interact with the Procore API.
    /// </summary>
    public class ProcoreClient
    {
        //---------------------------------------------------------------------
        // Properties - Public
        //---------------------------------------------------------------------

        /// <summary>
        ///     <see cref="CompanyClient" />.
        /// </summary>
        public CompanyClient CompanyClient { get; set; }

        /// <summary>
        ///     <see cref="CompanyUserClient" />.
        /// </summary>
        public CompanyUserClient CompanyUserClient { get; set; }

        /// <summary>
        ///     <see cref="CompanyVendorClient" />.
        /// </summary>
        public CompanyVendorClient CompanyVendorClient { get; set; }

        /// <summary>
        ///     <see cref="ProjectClient" />.
        /// </summary>
        public ProjectClient ProjectClient { get; set; }

        //---------------------------------------------------------------------
        // Constructor - Public
        //---------------------------------------------------------------------

        /// <summary>
        ///     Initializes a new instance of the <see cref="ProcoreClient" /> class.
        /// </summary>
        /// <param name="token">OAuth 2.0 token obtained from Procore.</param>
        /// <param name="httpClient"><see cref="HttpClient" /> used to perform the requests.</param>
        /// <exception cref="ArgumentNullException" />
        public ProcoreClient(string token, HttpClient httpClient = null)
        {
            // Determine if the token is null.
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }

            // Determine if the HTTP client is null.
            httpClient = httpClient ?? new HttpClient {BaseAddress = new Uri("https://api.procore.com")};
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Initialize the properties.
            CompanyClient = new CompanyClient(httpClient);
            CompanyUserClient = new CompanyUserClient(httpClient);
            CompanyVendorClient = new CompanyVendorClient(httpClient);
            ProjectClient = new ProjectClient(httpClient);
        }
    }
}