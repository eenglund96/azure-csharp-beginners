using System.Collections.Generic;
using System.Linq;
using GreetingConsoleApp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GreetingConsoleAppTest;

[TestClass]                                         //this attribute marks this class as an MSTEST test class
public class GreetingTemplateRepositoryTest
{
    [TestMethod]                                    //this attribute marks this method as an MSTEST test method
    public void new_greeting_template_repository_should_not_be_empty()
    {
        var repo = new GreetingTemplateRepository();

        Assert.IsNotNull(repo.GreetingTemplates);                           //assert that the property GreetingTemplates is not null
        Assert.IsTrue(repo.GreetingTemplates.Any());                        //assert the GreetingTemplates contains at least 1 object
    }

    [TestMethod]
    public void get_greeting_template_should_return_greeting()
    {
        var repo = new GreetingTemplateRepository();
        
        var template1 = repo.GetGreetingTemplate(1);
        Assert.AreEqual("A generic christmas greeting!", template1.Message);        //assert that key 1 is "A generic christmas greeting!". This test will break if init code in GreetingTemplateRepository is updated. Not good practice to have sensitive tests

        var template2 = repo.GetGreetingTemplate(2);
        Assert.AreEqual("A generic new year greeting!", template2.Message);         //assert that key 2 is "A generic new year greeting!". This test will break if init code in GreetingTemplateRepository is updated. Not good practice to have sensitive tests
    }

    [TestMethod]
    public void get_greeting_template_with_wrong_key_should_throw()
    {
        var repo = new GreetingTemplateRepository();
        Assert.ThrowsException<KeyNotFoundException>(() => 
        {
            var template = repo.GetGreetingTemplate(-1);                             //assert that this line of code throws an exception of type KeyNotFoundException
        });
    }

    [TestMethod]
    public void saved_greeting_template_should_be_retrievable()
    {
        var repo = new GreetingTemplateRepository();

        var template = new Greeting
        {
            Message = Guid.NewGuid().ToString(),                        //If possible, generate unique values in tests to avoid collisions or state changes
            From = Guid.NewGuid().ToString(),
            To = Guid.NewGuid().ToString(),
        };
        var key = repo.SaveGreetingTemplate(3, template);

        Assert.AreEqual(key, 3);
        
        var savedTemplate = repo.GetGreetingTemplate(3);

        Assert.IsNotNull(savedTemplate);
        Assert.AreEqual(template.Message, savedTemplate.Message);
        Assert.AreEqual(template.From, savedTemplate.From);
        Assert.AreEqual(template.To, savedTemplate.To);
    }

    [TestMethod]
    public void get_greeting_template_by_length_with_linq_test()
    {
        var repo = new GreetingTemplateRepository();
        var templates0 = repo.GetGreetingTemplatesByLengthWithLinq(0);

        Assert.AreEqual(2, templates0.Count());

        var templates29 = repo.GetGreetingTemplatesByLengthWithLinq(29);

        Assert.AreEqual(1, templates29.Count());
    }

    [TestMethod]
    public void get_greeting_template_by_length_with_lambda_test()
    {
        var repo = new GreetingTemplateRepository();
        var templates0 = repo.GetGreetingTemplatesByLengthWithLambda(0);

        Assert.AreEqual(2, templates0.Count());

        var templates29 = repo.GetGreetingTemplatesByLengthWithLambda(29);

        Assert.AreEqual(1, templates29.Count());
    }

    [TestMethod]
    public void get_greeting_template_by_length_with_foreach_test()
    {
        var repo = new GreetingTemplateRepository();
        var templates0 = repo.GetGreetingTemplatesByLengthWithForeach(0);

        Assert.AreEqual(2, templates0.Count());

        var templates29 = repo.GetGreetingTemplatesByLengthWithForeach(29);

        Assert.AreEqual(1, templates29.Count());
    }

    [TestMethod]
    public void get_greeting_template_by_length_methods_should_equal()
    {
        var repo = new GreetingTemplateRepository();

        var length = 29;
        var linqResult = repo.GetGreetingTemplatesByLengthWithLinq(length);
        var lambdaResult = repo.GetGreetingTemplatesByLengthWithLambda(length);
        var foreachResult = repo.GetGreetingTemplatesByLengthWithForeach(length);

        Assert.AreEqual(linqResult.Count(), lambdaResult.Count());
        Assert.AreEqual(linqResult.Count(), foreachResult.Count());
        Assert.AreEqual(lambdaResult.Count(), foreachResult.Count());
    }

    [TestMethod]
    public void get_greeting_template_by_search_string_with_linq_test()
    {
        var repo = new GreetingTemplateRepository();

        var template1 = new Greeting
        {
            Message = Guid.NewGuid().ToString(),
        };

        repo.SaveGreetingTemplate(10000, template1);            //we create the state we want to test to avoid external dependencies

        var template2 = new Greeting
        {
            Message = Guid.NewGuid().ToString(),            
        };

        repo.SaveGreetingTemplate(10001, template2);
        repo.SaveGreetingTemplate(10002, template2);            //save this twice and assert that it returns both
        
        var template1Result = repo.GetGreetingTemplatesBySearchStringWithLinq(template1.Message);
        Assert.AreEqual(1, template1Result.Count());
        Assert.AreEqual(template1.Message, template1Result.First().Message);

        var template2Result = repo.GetGreetingTemplatesBySearchStringWithLinq(template2.Message);
        Assert.AreEqual(2, template2Result.Count());
        Assert.AreEqual(template2.Message, template2Result.First().Message);
    }
}