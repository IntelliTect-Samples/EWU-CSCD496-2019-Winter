﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SecretSanta.Domain.Models
{
    public class Gift : Entity
    {
        public Gift(string title, int orderOfImportance, string url, string description, User user)
        {
            Title = title;
            OrderOfImportance = orderOfImportance;
            Url = url;
            Description = description;
            User = user;
        }

        private string _Title;
        public string Title
        {
            get => _Title;
            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException("value");
                }

                value = value.Trim();

                if (value is "")
                {
                    throw new ArgumentException("Title cannot be empty.", "value");
                }

                _Title = value;
            }
        }

        private int _OrderOfImportance;
        public int OrderOfImportance
        {
            get => _OrderOfImportance;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException("OrderOfImportance must be at least 1.", "value");
                }
            }
        }
        public string Url { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        
    }
}