﻿using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineBookStore.Controllers;
using OnlineBookStore.DataAccess;
using OnlineBookStore.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace OnlineBookStore.Services
{
    public class ServiceClass : IServiceClass
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ServiceClass> _logger;

        public ServiceClass(AppDbContext context, ILogger<ServiceClass> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Tuple<decimal, string> CalculateRevenue(DateTime startDate, DateTime endDate)
        {
            decimal totalRevenue = 0;
            string errormessage = "";
            try
            {
                if (startDate > endDate)
                {
                    throw new ArgumentException("Start date cannot be greater than end date.");
                }

                foreach (var book in _context.Books)
                {
                    if (book.SaleDate >= startDate && book.SaleDate <= endDate)
                    {
                        totalRevenue += book.Revenue;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred calculating revenue {ErrorMessage}", ex.Message);
                errormessage = ex.Message;
            }
            return Tuple.Create(totalRevenue, errormessage);
        }

        public Tuple<Book?, string> DeleteBooks(List<int> bookIds)
        {
            Book book = null;
            string errormessage = "";
            try
            {
                foreach (var id in bookIds)
                {
                    Book? bookToRemove = _context.Books.FirstOrDefault(book => book.BookId == id);
                    if (bookToRemove != null)
                    {
                        _context.Books.Remove(bookToRemove);
                        _context.SaveChanges();
                        _logger.LogInformation(($"Book with ID {id} deleted.");

                    }
                    else
                    {
                        _logger.LogInformation(($"Book with ID {id} not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the book {ErrorMessage}", ex.Message);
                errormessage = ex.Message;
            }
            return Tuple.Create(book, errormessage);
        }

        public Tuple<IEnumerable<Author>, string> GetTopSellingAuthors()
        {
            IEnumerable<Author> topAuthors = null;
            string errorMessage = "";
            try
            {
                topAuthors = _context.Authors
                    .OrderByDescending(author => author.TotalSales)
                    .Take(10)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching records. {ErrorMessage}", ex.Message);
                errorMessage = ex.Message;
            }
            return Tuple.Create(topAuthors, errorMessage);
        }
    }
}