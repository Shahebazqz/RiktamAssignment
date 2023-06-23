UserController

To call each service from the API controller, you can use HTTP requests to the corresponding endpoints defined in the controller. Here's an example of how to call each service:

Authenticating a user:

HTTP method: POST
Endpoint: /api/User/authenticate
Request body: User object containing the username and password
Response: Returns the authenticated user object with a JWT token
Creating a user:

HTTP method: POST
Endpoint: /api/User/CreateUser
Request body: UserDto object containing user details
Response: Returns the created user object
Getting all users:

HTTP method: GET
Endpoint: /api/User/GetUser
Response: Returns a list of UserDto objects representing all users
Getting a specific user by ID:

HTTP method: GET
Endpoint: /api/User/GetUser/{id}
Response: Returns the UserDto object corresponding to the provided ID
Getting user groups by user ID:

HTTP method: GET
Endpoint: /api/User/GetUserGroups/{id}
Response: Returns a list of GroupDto objects representing the groups associated with the user
Deleting a user by ID:

HTTP method: DELETE
Endpoint: /api/User/DeleteUser/{id}
Response: Returns a success message if the deletion is successful; otherwise, returns an error message
Updating a user by ID:

HTTP method: PUT
Endpoint: /api/User/UpdateUser/{id}
Request body: User object containing the updated user details
Response: Returns a success message if the update is successful; otherwise, returns an error message
Make sure to replace {id} with the actual user ID when making requests to the endpoints that require it. You can use libraries like HttpClient in C# to make these HTTP requests to the API controller from your application.


Group Controller

Creating a group:

HTTP method: POST
Endpoint: /api/Group
Request body: GroupDto object containing group details
Response: Returns the created Group object
Getting a specific group by ID:

HTTP method: GET
Endpoint: /api/Group/GetGroup/{id}
Response: Returns the Group object corresponding to the provided ID
Deleting a group by ID:

HTTP method: DELETE
Endpoint: /api/Group/DeleteGroup/{id}
Response: Returns a success message if the deletion is successful; otherwise, returns an error message
Updating a group by ID:

HTTP method: PUT
Endpoint: /api/Group/UpdateGroup/{id}
Request body: GroupDto object containing the updated group details
Response: Returns the updated Group object if the update is successful; otherwise, returns an error message
Adding a member to a group:

HTTP method: PUT
Endpoint: /api/Group/AddMemberToGroup/{id}
Request body: UserDto object containing the member details
Response: Returns the updated Group object with the added member if successful; otherwise, returns an error message
Getting all groups:

HTTP method: GET
Endpoint: /api/Group/GetGroup
Response: Returns a list of Group objects representing all groups
Make sure to replace {id} with the actual group ID when making requests to the endpoints that require it. You can use libraries like HttpClient in C# to make these HTTP requests to the API controller from your application.

GroupMessage Controller

Creating a group message:

HTTP method: POST
Endpoint: /api/GroupMessage/CreateGroupMessage
Request body: GroupMessageDto object containing the group message details
Response: Returns the created GroupMessage object if successful; otherwise, returns an error message
Getting a specific group message by ID:

HTTP method: GET
Endpoint: /api/GroupMessage/GetGroupMessage/{id}
Response: Returns the GroupMessage object corresponding to the provided ID if found; otherwise, returns a not found message
Deleting a group message by ID:

HTTP method: DELETE
Endpoint: /api/GroupMessage/DeleteGroupMessage/{id}
Response: Returns a success message if the deletion is successful; otherwise, returns a not found message
Updating a group message by ID:

HTTP method: PUT
Endpoint: /api/GroupMessage/UpdateGroupMessage/{id}
Request body: GroupMessageDto object containing the updated group message details
Response: Returns the updated GroupMessage object if successful; otherwise, returns an error message
Getting all group messages:

HTTP method: GET
Endpoint: /api/GroupMessage/GetGroupMessages
Response: Returns a list of GroupMessage objects representing all group messages
Make sure to replace {id} with the actual group message ID when making requests to the endpoints that require it. You can use libraries like HttpClient in C# to make these HTTP requests to the API controller from your application.

LikedMessage Controller

Liking a message:

HTTP method: POST
Endpoint: /api/LikedMessage/LikeMessage/{id}
Request body: LikedMessage object containing the message ID and username
Response: Returns the LikedMessage object if successful; otherwise, returns an error message
Getting the likes for a specific message:

HTTP method: GET
Endpoint: /api/LikedMessage/GetMessageLikes/{messageId}
Response: Returns a list of LikedMessage objects representing the likes for the provided message ID
Getting the likes by a user:

HTTP method: GET
Endpoint: /api/LikedMessage/GetUserLikes/{username}
Response: Returns a list of LikedMessage objects representing the likes by the provided username
Deleting a like by message ID and username:

HTTP method: DELETE
Endpoint: /api/LikedMessage/DeleteMessageLike/{messageId}/{username}
Response: Returns a success message if the deletion is successful; otherwise, returns a not found message
Make sure to replace {id}, {messageId}, and {username} with the actual values when making requests to the endpoints that require them. You can use libraries like HttpClient in C# to make these HTTP requests to the API controller from your application.


