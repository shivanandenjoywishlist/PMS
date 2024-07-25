using PMS_BAL.IService.Common;
using System;
using System.Reflection;

namespace PMS_BAL.Service.Common
{
    public class BaseService : IBaseService
    {
        public T ExcecuteFunction<T>(Func<T> method)
        {
            T obj = default(T);
            try
            {
                return method();
            }
            catch (Exception ex)
            {
                // Handle exception gracefully

                // Attempt to create an instance of T if possible
                try
                {
                    obj = Activator.CreateInstance<T>();
                }
                catch (Exception createInstanceEx)
                {
                    // Log or handle the inability to create an instance of T
                    throw new Exception("Error creating instance of type T", createInstanceEx);
                }

                // Set default values for properties if they are of a known type
                foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
                {
                    try
                    {
                        // Handle specific properties based on their names or types
                        switch (propertyInfo.Name)
                        {
                            case "Data":
                                // Set default value for 'Data' property (if applicable)
                                propertyInfo.SetValue(obj, null);
                                break;
                            case "Message":
                                // Set default value for 'Message' property (if applicable)
                                propertyInfo.SetValue(obj, "Internal Server Error");
                                break;
                            case "StatusCode":
                                // Set default value for 'StatusCode' property (if applicable)
                                propertyInfo.SetValue(obj, 500);
                                break;
                            default:
                                // Optionally handle other properties here
                                break;
                        }
                    }
                    catch (Exception propertyEx)
                    {
                        // Log or handle exceptions that occur during property handling
                        throw new Exception($"Error handling property '{propertyInfo.Name}'", propertyEx);
                    }
                }
                // Rethrow the original exception to propagate it up the call stack
                throw new Exception("Unhandled exception occurred", ex);
            }
        }
    }
}
