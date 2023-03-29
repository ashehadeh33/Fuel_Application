﻿using qenergy.Models;
using qenergy.Data;
using Microsoft.EntityFrameworkCore;

namespace qenergy.Services
{
    public class AccountService
    {
        private readonly qEnergyContext _context;
        public AccountService(qEnergyContext context)
        {
            _context = context;
        }

        // the Users collection contains all the rows in the Users table and the Quotes collection contains all the rows in the Quotes table.
        // the AsNoTracking extension method instructs EF Core to disable change tracking. Since this operation is read-only, AsNoTracking can optimize performance
        // all of the Users and Quotes are returned with ToList
        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users
                .AsNoTracking()
                .ToList();
        }
        public IEnumerable<Quote> GetAllQuotes()
        {
            return _context.Quotes
                .AsNoTracking()
                .ToList();
        }

        // The Include extensions method takes a lambda expression to specify that the Toppings and Sauce navigation properties are to be included in the result. Without this, EF Core will return null for those properties
        // The SingleOrDefault method returns a pizza that matches the lambda expression. -if no records match, null is returned -if multiple records match, an exception is thrown -the lambda expression describes records where the Id prop == id parameter
        public User? GetUserById(int id)
        {
            return _context.Users
                .Include(u => u.Username)
                .Include(u => u.Password)
                .Include(u => u.profile)
                .AsNoTracking()
                .SingleOrDefault(u => u.Id == id);
        }
        public Quote? GetQuoteById(int id)
        {
            return _context.Quotes
                .Include(q => q.customerId)
                .Include(q => q.DeliveryAddress)
                .Include(q => q.DeliveryDate)
                .Include(q => q.GallonsRequested)
                .Include(q => q.SuggestedPricePerGallon)
                .Include(q => q.TotalAmountDue)
                .AsNoTracking()
                .SingleOrDefault(q => q.Id == id);
        }

        // newUser is assumed to be a valid object. EF Core doesn't do data validation, so any validation must be handled by the ASP.NET Core runtime or user code
        // The Add method adds the newUser entity to EF Core's object graph 
        // The SaveChanges method instructs EF Core to presist the object changes to the database
        public User? CreateUser(User newUser)
        {
            // TODO: make sure a profile is created and tied to this UserID later or something
            // maybe if the user creates a user but exits before creating a profile, then the next time they log in, it will redirect to the createProfile page again
            // for now, i think we can keep it like this just so that we can create a database with certain columns
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return newUser;
        }
        public Quote? CreateQuote(Quote newQuote)
        {
            _context.Quotes.Add(newQuote);
            _context.SaveChanges();

            return newQuote;
        }

        // References to an existing User is created using Find. Find is an optimized method to query records by their primary key. Find serches the local entity graph first before querying the database
        // The User.profile prop is set to the newProfile object
        // An Update method call is unnecessary because EF Core detects that we set the Sauce property on Pizza
        // The SaveChanges method instructs EFCore to persist the object changes to the database
        public void bindProfileToUser(Profile newProfile, int userId)
        {
            var userToUpdate = _context.Users.Find(userId);
            if (userToUpdate is null)
            {
                throw new InvalidOperationException("User does not exist");
            }
            userToUpdate.profile = newProfile;

            _context.SaveChanges();
        }

        // Find retrieves a User by the primary key (in this case, id)
        // Remove method removes the userToDelete entity in EF Core's object graph
        // SaveChanges instructs EF Core to persist the object changes to the database.
        public void DeleteUserById(int id)
        {
            var userToDelete = _context.Users.Find(id);
            if (userToDelete is not null)
            {
                _context.Users.Remove(userToDelete);
                _context.SaveChanges();
            }
        }
        public void DeleteQuoteById(int id)
        {
            var quoteToDelete = _context.Quotes.Find(id);
            if (quoteToDelete is not null)
            {
                _context.Quotes.Remove(quoteToDelete);
                _context.SaveChanges();
            }
        }

    }
}

