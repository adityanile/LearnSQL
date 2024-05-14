This is a completely functional Andriod Game Developed in Unity 2D using C#, NodeJS (NextJS), Typescript, PrismaORM, Postgres Database.
To help you learn SQL in the simplest way.

There are Three Categories : 
                                1. Theory 2. Coding 3. Interview
                                - Each category Have 6 distinct levels for the user to clear
                                - Each level have 10 Distinct Question.
Features:
            - This is a complete dynamic game, So owner does not have push multiple updates to user to install again and again, He can directly use an api(/addQuestion)
              to Add question in database ( ofcourse he needs to have valid credentails to modify database).
            - The added Questions are directly fetched from the server to the user every time he takes a session in the game  
            - The Questions and fetched randomly from the database and shown to user in many different combinations.
            - The Database is custom Designed and Cloud Hosted on vercel Postgres Account (Owned By Me)
            - The Schema for Database and api are customed designed in Typescript (NextJS) ( Included in Api Folder in Root Directory).
            - You can run the Application from Unity Editor (2022.3.24)
            - The Application is also integrated with feature of local Storage to save the progress of the player.

- The Main Attention in given to design the mechanices of the game and backend of the game, on how the data will be handeled in the application
- The data is handeled in JSON format for Simplicity.
- Also attention is given to smaller deatils in UI like,
                                                            * You cannot select multiple options at single time
                                                            * Incase of wrong answer user will be shown with correct answer for next time
                                                            * Only if the current session scored is higher than previous score it is updated in local Storage
                                                              and same is shown to user.
                                                            * Also correct score of session is shown to user.

  // Utilities
- Body of JSON To Add question
- POST /addQuestion
- {
  "que" : "Your Age ?",
  "op1" : "10",
  "op2" : "20",
  "op3" : "30",
  "op4" : "40",
  "correct" : "10",
  "level" : 1,
  "tag" : "Theory"
}

- Select Level and tag according to the need (Remember index of level starts from 0).
- Tag is in ["Theory","Coding","Interview"]

- Body of JSON To Fetch Question
- Post /getMeQuestions
- {
  "level" : 0,
  "tag" : "Interview"
}

         

