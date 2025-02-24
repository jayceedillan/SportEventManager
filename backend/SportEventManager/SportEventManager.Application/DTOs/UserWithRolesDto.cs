﻿namespace SportEventManager.Application.DTOs
{
    public class UserWithRolesDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
