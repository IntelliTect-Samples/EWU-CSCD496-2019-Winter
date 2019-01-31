using Microsoft.EntityFrameworkCore;
using SecretSanta.Domain.Interfaces;
using SecretSanta.Domain.Models;
using System;
using System.Collections.Generic;

namespace SecretSanta.Domain.Services
{
    public class UserService : IUserService
    {
        private SecretSantaDbContext DbContext { get; }

        public UserService(SecretSantaDbContext context)
        {
            DbContext = context;
        }

        public bool UpsertUser(User user)
        {
            if (user != null)
            {

                if (user.Id == default(int))
                {
                    DbContext.Users.Add(user);
                }
                else
                {
                    DbContext.Users.Update(user);
                }
                DbContext.SaveChanges();
                return true;
            }
            else
                return false;
        }

        public bool MakeUser(string info)
        {
            bool test = false;
            bool hasComma = false;
            string[] name = new string[2];
            string[] temp;

            if (info.Contains(":"))
            {
                temp = info.Split(':');
            }
            else
            {
                return test;
            }

            if (temp.Length == 2 && temp[0].Length != 0 && temp[1].Length != 0)
            {
                temp[1] = temp[1].Trim();

                if (temp[1].Contains(","))
                {
                    temp = temp[1].Split(',');
                    hasComma = true;
                }
                else if (temp[1].Contains(" "))
                {
                    temp = temp[1].Split(' ');
                }
                else
                {
                    return test;
                }
            }
            else
            {
                return test;
            }

            if (temp[0].Length == 0 || temp[1].Length == 0)
            {
                return test;
            }

            temp[0] = temp[0].Trim();
            temp[1] = temp[1].Trim();

            if (hasComma)
            {
                name[0] = temp[1];
                name[1] = temp[0];
            }
            else
            {
                name = temp;
            }

            test = UpsertUser(new User() { First = name[0], Last = name[1] });

            return test;
        }

        public User Find(int id)
        {
            return DbContext.Users.Find(id);
        }

        public List<User> FetchAll()
        {
            var userTask = DbContext.Users.ToListAsync();
            userTask.Wait();

            return userTask.Result;
        }

        public bool DeleteUser(int id)
        {
            bool test = false;

            User user = Find(id);

            if (user != null)
            {
                DbContext.Remove(user);
                DbContext.SaveChanges();
                test = true;
            }

            return test;
        }
    }
}
