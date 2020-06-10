# GajGamesRouterService
Microservice to route requests to the other services  involved. In example: a user signs up and uploads a profile image, so the gateway routes to the AuthService and this one stores the new user and sends the image to the ImageService through this router. It will also get AccountData from the AccountService and add it to the UserDto which will be returned to the frontend. 
