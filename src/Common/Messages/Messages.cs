namespace Common
{
    public static class Messages
    {
        #region Common Messages

        public static readonly string AppName = "-------ToDoApp-------";

        public static readonly string Instructions = "Select option from the menu and press enter:";

        public static readonly string DatabaseError = "Database Error Occurred!";

        public static readonly string InvalidCommad = "Invalid command!";

        public static readonly string NotLogged = "You are not logged in!";

        #endregion

        #region User Management Messages

        public static readonly string WrongInitialCredentials = "Wrong username or password! Initial login must be executed with administrator credentials!";

        public static readonly string WrongCredentials = "Wrong username or password!";

        public static readonly string SuccessfulLogin = "Login Successful!";

        public static readonly string UnableToCreateInitialUser = "Unable To Create Initial User!";

        public static readonly string SuccessfulLogout = "Logout Successful!";

        public static readonly string AdminAccessDenied = "Only admin users have access to this functionality!";

        public static readonly string AdminOrSharedAccessDenied = "Only admin or shared users have access to this functionality!";

        public static readonly string UserCreationSuccessfull = "User has been successfully created!";

        public static readonly string UserDoesntExist = "User doesnt exist!";

        public static readonly string UserEditSuccessfull = "User has been successfully edited!";

        public static readonly string UnableToEditUser = "Unable To Edit User!";

        public static readonly string UserAlreadyExist = "User already exist!";

        public static readonly string UserInvalidId = "Invalid User Id!";

        public static readonly string UsersEmptyCollection = "Users collection is empty!";

        public static readonly string UserInitialAdminDeletingUnsuccessful = "Cant delete initial admin user!";

        public static readonly string UserCurrentDeletingUnsuccessful = "Can't delete current user!";

        public static readonly string UserCurrentEditingUnsuccessful = "Can' edit current user!";

        public static readonly string UserInitialEditingUnsuccessful = "Can't edit initial user!";

        public static readonly string UserDeletedSuccessfull = "User has been successfully deleted!";

        public static readonly string UnableToDeleteUser = "Unable To Delete User!";

        public static readonly string UserDontHaveRightsToDeleteToDoList = "Current user dont have rights to delete ToDo List!";

        public static readonly string UserDontHaveRightsToEditToDoList = "Current user dont have rights to edit ToDo List!";

        public static readonly string UserDontHaveRightsToShareToDoList = "Current user dont have rights to share ToDo List!";

        public static readonly string UserAlreadyHaveShareToToDoListOrIsCreator = "User already have ToDo List shared, or he is the creator ot the ToDo List!";

        public static readonly string UserDontHaveAccessOrDoestExist = "User doesn't exist or dont have access to ToDo List!";

        public static readonly string UsersList = "Users list:";

        public static readonly string UnableToCreateUser = "Unable To Create User";


        #endregion
    }
}
