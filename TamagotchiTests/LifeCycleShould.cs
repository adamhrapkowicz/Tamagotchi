using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TamagotchiUnitTests
{
    internal class LifeCycleShould
    {
        //[Fact]
        //public void ProgressesLifeOnProgressLifeCall()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;

        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().BeGreaterThan(startingAge);
        //    testDragon.Happiness.Should().BeLessThan(startingHappiness);
        //    testDragon.Feedometer.Should().BeLessThan(startingFeedometer);
        //}

        //[Fact]
        //public void ChangeDragonIsAliveToFalseOnProgressLifeCallIfMinFeedometerReached()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;
        //    testDragon.Feedometer = _gameSettings.MinValueOfFeedometer;


        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().BeGreaterThan(startingAge);
        //    testDragon.Happiness.Should().BeLessThan(startingHappiness);
        //    testDragon.Feedometer.Should().BeLessThan(startingFeedometer);
        //    testDragon.IsAlive.Should().BeFalse();
        //}

        //[Fact]
        //public void ChangeDragonIsAliveToFalseOnProgressLifeCallIfMinHappinessReached()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;
        //    testDragon.Happiness = _gameSettings.MinValueOfHappiness;

        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().BeGreaterThan(startingAge);
        //    testDragon.Happiness.Should().BeLessThan(startingHappiness);
        //    testDragon.Feedometer.Should().BeLessThan(startingFeedometer);
        //    testDragon.IsAlive.Should().BeFalse();
        //}

        //[Fact]
        //public void ChangeDragonIsAliveToFalseOnProgressLifeCallIfMaxAgeReached()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;
        //    testDragon.Age = _gameSettings.MaxAge;

        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().BeGreaterThan(startingAge);
        //    testDragon.Happiness.Should().BeLessThan(startingHappiness);
        //    testDragon.Feedometer.Should().BeLessThan(startingFeedometer);
        //    testDragon.IsAlive.Should().BeFalse();
        //}

        //[Fact]
        //public void NotProgressLifeOnProgressLifeCallIfDragonIsNotAlive()
        //{
        //    //Arrange
        //    var testDragonId = _lifeCycleManager.CreateDragon("TestDragon1");
        //    var testDragon = _lifeCycleManager.GetDragonById(testDragonId);
        //    var startingAge = testDragon.Age;
        //    var startingHappiness = testDragon.Happiness;
        //    var startingFeedometer = testDragon.Feedometer;
        //    testDragon.IsAlive = false;

        //    //Act
        //    _lifeCycleManager.ProgressLife();

        //    //Assert
        //    testDragon.Age.Should().Be(startingAge);
        //    testDragon.Happiness.Should().Be(startingHappiness);
        //    testDragon.Feedometer.Should().Be(startingFeedometer);
        //}
    }
}
