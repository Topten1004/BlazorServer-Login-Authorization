using BlazorBaseApp.Model;
using BlazorBaseApp.Repositories.Interfaces;
using BlazorBaseApp.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace BlazorBaseApp.Repositories
{

    public class PersonRepository : IPersonRepository
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;
        public PersonRepository(DataContext db, IMapper mapper)
        {
            _mapper = mapper;
            _dataContext = db;
        }
        public async Task<PersonModel> CheckForDuplicates(string sUserName)
        {
            try
            {
                    PersonModel person = _mapper.Map<Person, PersonModel>(await _dataContext.Persons.FirstOrDefaultAsync(x => x.UserName.ToLower() == sUserName.ToLower()));
                    return person;
                
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<PersonModel> CreatePerson(PersonModel _Person)
        {
            Person person = _mapper.Map<PersonModel, Person>(_Person);

            person.Password = GetMd5Hash(person.Password);

            var AddedPerson = await _dataContext.Persons.AddAsync(person);
            await _dataContext.SaveChangesAsync();
            return _mapper.Map<Person, PersonModel>(AddedPerson.Entity);
        }

        public static string GetMd5Hash(string input)
        {
            using (var md5 = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
                // Create a new StringBuilder to collect the bytes
                // and create a string.
                var builder = new StringBuilder();
                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    builder.Append(data[i].ToString("x2"));
                }
                // Return the hexadecimal string.
                return builder.ToString();
            }
        }

        public async Task<bool> DeletePerson(int PersonID)
        {
            var PersonDetails = await _dataContext.Persons.FindAsync(PersonID);
            if (PersonDetails != null)
            {
                _dataContext.Remove(PersonDetails);
                return true;
            }
            return false;
        }

        public async Task<PersonModel> GetPerson(string UserName, string Password)
        {
            try
            {
                Person person = await _dataContext.Persons.FirstOrDefaultAsync(x => x.UserName == UserName);

                if (VerifyMd5Hash(Password, person.Password))
                {
                    PersonModel Person1 = _mapper.Map<Person, PersonModel>(person);
                    return Person1;
                }
                else
                    return null;
            }
            catch
            {
                return null;
            }
        }

        public static bool VerifyMd5Hash(string input, string hash)
        {
            using (var md5 = MD5.Create())
            {
                // Hash the input.
                string hashOfInput = GetMd5Hash(input);

                // Create a StringComparer an compare the hashes.
                StringComparer comparer = StringComparer.OrdinalIgnoreCase;

                if (0 == comparer.Compare(hashOfInput, hash))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<PersonModel> UpdatePerson(int PersonID, PersonModel _Person)
        {
            try
            {
                if (PersonID == _Person.Id)
                {
                    Person persondetails = await _dataContext.Persons.FindAsync(PersonID);
                    Person person = _mapper.Map<PersonModel, Person>(_Person, persondetails);
                    var UpdatePerson = _dataContext.Persons.Update(person);
                    await _dataContext.SaveChangesAsync();
                    return _mapper.Map<Person, PersonModel>(UpdatePerson.Entity);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
