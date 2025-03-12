using Faces.Domain.Entities;

namespace Faces.UnitTest
{
    public class EmployeeUnitTest
    {


        [Fact]
        public void ShouldFailWhenValidatingEmployeeRequiredFields()
        {
            var employee = new Employee();

            (bool isValid, string message) = employee.HasRequiredFields();

            Assert.False(isValid, message);
            Assert.Equal("The following fields are missing: first name, last name, email, document number, birth date, job function", message);
        }

        [Fact]
        public void ShouldCalculateCorrectAge()
        {
            var employee = new Employee();

            int age = 10;
            var birthDate = DateTime.Today.AddYears(-age);

            employee.BirthDate = DateOnly.FromDateTime(birthDate);

            Assert.Equal(age,employee.Age);
        }

        [Fact]
        public void ShouldHaveAgeOfMajority()
        {
            var employee = new Employee();
            int age = 18;
            var birthDate = DateTime.Today.AddYears(-age);
            employee.BirthDate = DateOnly.FromDateTime(birthDate);

            Assert.True(employee.IsAgeOfMajority);
        }

        [Fact]
        public void ShouldIndicateMinorAge()
        {
            var employee = new Employee();
            int age = 17;
            var birthDate = DateTime.Today.AddYears(-age);
            employee.BirthDate = DateOnly.FromDateTime(birthDate);

            Assert.False(employee.IsAgeOfMajority);
        }



        [Fact]
        public void ShouldFailForMissingManager()
        {
            var employee = new Employee();
            employee.JobFunction = new JobFunction("0001", "Analyst", 10);

            (bool isValid, string message) = employee.HasManagerOrIsHighestJobFunction();

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldFailForHighestHierarchyLevelJobFunctionWithManager()
        {
            var employee = new Employee();
            employee.JobFunction = new JobFunction("0002", "CEO", JobFunction.HIGH_HIERARCHY);
            employee.Manager = new Employee();

            (bool isValid, string message) = employee.HasManagerOrIsHighestJobFunction();

            Assert.False(isValid);
        }

        [Fact]
        public void ShouldSucceedForHighestHierarchyLevelJobFunction()
        {
            var employee = new Employee();
            employee.JobFunction = new JobFunction("0002", "CEO", JobFunction.HIGH_HIERARCHY);

            (bool isValid, string message) = employee.HasManagerOrIsHighestJobFunction();

            Assert.True(isValid);
        }

        [Fact]
        public void ShouldSucceedForLowHierarchyLevelWithManager()
        {
            var employee = new Employee();
            employee.JobFunction = new JobFunction("0003", "Tech Leader", 100);
            employee.Manager = new Employee();

            (bool isValid, string message) = employee.HasManagerOrIsHighestJobFunction();

            Assert.True(isValid);
        }
    }
}