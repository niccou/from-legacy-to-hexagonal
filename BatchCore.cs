namespace MyLegacyApp
{
    public class BatchCore
    {
        public void DoSomeStuff()
        {
            // -- If this is a new customer, create the customer record --
            // Determine whether the customer is an existing customer.
            // If not, validate entered customer information
            // If not valid, notify the user.
            // If valid,
            // Open a connection
            // Set stored procedure parameters with the customer data.
            // Call the save stored procedure.

            // -- Create the order for the customer. --
            
            // Validate the entered information.
            // If not valid, notify the user.
            // If valid,
            // Open a connection
            // Set stored procedure parameters with the order data.
            // Call the save stored procedure.

            // -- Order the items from inventory --
            // For each item ordered,
            // Locate the item in inventory.
            // If no longer available, notify the user.
            // If any items are back ordered and
            // the customer does not want split orders,
            // notify the user.
            // If the item is available,
            // Decrement the quantity remaining.
            // Open a connection
            // Set stored procedure parameters with the inventory data.
            // Call the save stored procedure.

            // -- Send an email receipt --
            // If the user requested a receipt
            // Get the customer data
            // Ensure a valid email address was provided.
            // If not,
            // request an email address from the user.
            // Open a connection
            // Set stored procedure parameters with the customer data.
            // Call the save stored procedure.

            // If a valid email address is provided,
            // Send an email.
        }
    }
}