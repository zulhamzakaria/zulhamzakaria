﻿using CloudCustomers.API.Models;
using System;

public interface IUsersService
{
    public Task<List<User>> GetAllUsers();
}

namespace CloudCustomers.API.Services
{
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        public UsersService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public Task<List<User>> GetAllUsers()
        {
            throw new NotImplementedException();
        }
    }
}
