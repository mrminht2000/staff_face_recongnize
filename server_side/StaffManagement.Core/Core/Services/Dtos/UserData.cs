﻿using StaffManagement.Core.Core.Persistence.Models;
using System;
using System.Collections.Generic;

namespace StaffManagement.Core.Core.Services.Dtos
{
    public class UserData
    {
        public string UserName { get; set; }
        public long Id { get; set; }
        public string FullName { get; set; }
        public int Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Department? Department { get; set; }
        public Job? Job { get; set; }
        public WorkingProgress? WorkingProgress { get; set; }
        public List<Event>? Events { get; set; }
        public DateTime StartDay { get; set; }
        public bool IsConfirmed { get; set; }
        public int? Status { get; set; }

        public UserData(User user)
        {
            UserName = user.UserName;
            Id = user.Id;
            FullName = user.FullName;
            Role = user.Role;
            PhoneNumber = user.PhoneNumber;
            Email = user.Email;
            Department = user.Department;
            Job = user.Job;
            WorkingProgress = user.WorkingProgress;
            Events = user.Events;
            StartDay = user.StartDay;
            IsConfirmed = user.IsConfirmed;
        }
    }
}
