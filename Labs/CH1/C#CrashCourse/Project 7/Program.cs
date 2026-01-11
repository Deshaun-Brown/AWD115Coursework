using System;
using System.Collections.Generic;

Dictionary<string, string> contacts = new Dictionary<string, string>();
bool running = true;

while (running)
{
    Console.WriteLine("\n=== Contact Manager ===");
    Console.WriteLine("1. Add Contact");
    Console.WriteLine("2. View Contact");
    Console.WriteLine("3. Update Contact");
    Console.WriteLine("4. Delete Contact");
    Console.WriteLine("5. List All Contacts");
    Console.WriteLine("6. Exit");
    Console.Write("\nSelect an option (1-6): ");

    string choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            AddContact();
            break;
        case "2":
            ViewContact();
            break;
        case "3":
            UpdateContact();
            break;
        case "4":
            DeleteContact();
            break;
        case "5":
            ListAllContacts();
            break;
        case "6":
            running = false;
            Console.WriteLine("Goodbye!");
            break;
        default:
            Console.WriteLine("Invalid option. Please try again.");
            break;
    }
}

void AddContact()
{
    Console.Write("Enter contact name: ");
    string name = Console.ReadLine();

    if (contacts.ContainsKey(name))
    {
        Console.WriteLine($"Contact '{name}' already exists. Use Update instead.");
        return;
    }

    Console.Write("Enter phone number: ");
    string phone = Console.ReadLine();

    contacts.Add(name, phone);
    Console.WriteLine($"Contact '{name}' added successfully!");
}

void ViewContact()
{
    Console.Write("Enter contact name: ");
    string name = Console.ReadLine();

    if (contacts.ContainsKey(name))
    {
        Console.WriteLine($"Name: {name}");
        Console.WriteLine($"Phone: {contacts[name]}");
    }
    else
    {
        Console.WriteLine($"Contact '{name}' not found.");
    }
}

void UpdateContact()
{
    Console.Write("Enter contact name: ");
    string name = Console.ReadLine();

    if (contacts.ContainsKey(name))
    {
        Console.Write("Enter new phone number: ");
        string phone = Console.ReadLine();

        contacts[name] = phone;
        Console.WriteLine($"Contact '{name}' updated successfully!");
    }
    else
    {
        Console.WriteLine($"Contact '{name}' not found.");
    }
}

void DeleteContact()
{
    Console.Write("Enter contact name: ");
    string name = Console.ReadLine();

    if (contacts.Remove(name))
    {
        Console.WriteLine($"Contact '{name}' deleted successfully!");
    }
    else
    {
        Console.WriteLine($"Contact '{name}' not found.");
    }
}

void ListAllContacts()
{
    if (contacts.Count == 0)
    {
        Console.WriteLine("No contacts in the list.");
        return;
    }

    Console.WriteLine("\n=== All Contacts ===");
    foreach (var contact in contacts)
    {
        Console.WriteLine($"{contact.Key}: {contact.Value}");
    }
}