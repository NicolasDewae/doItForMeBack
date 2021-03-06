# doItForMeBack
## Etapes pour installation:
### Paramétrez la chaine de connection
Dans MySQL, créez une base de données nommée DoItForMeDatabase. <br>
Au sein du projet, allez dans appsettings.json et personnalisez la phrase de "ConnectionStrings" avec votre propre user et password 

### Effectuer et executer les migrations 

Ouvrir la console:
outils -> Gestionnaire de package NuGet -> Console du Gestionnaire de package

Executez la migration pour créer ou mettre à jour le schéma de la base de données.
```shell
update-database
```

### Céer votre premier utilisateur
Lancer le projet.<br>
Dans swagger utilisez la méthode "Registration" située dans la section Security pour créer un admin. Renseignez les champs obligatoires et dans rôle, notez bien "Admin" commençant par une majuscule.

Ressources:

- [initialiser EF Core](https://dev.to/renukapatil/create-web-api-with-aspnet-core-60-46l4).
- [Gérer l'authentification avec JWT](https://jasonwatmore.com/post/2021/12/14/net-6-jwt-authentication-tutorial-with-example-api);

- [Commenter son code avec .Net](https://vincentlaine.developpez.com/tuto/dotnet/comdoc/#LIII-B-1).
- [Nommer ses branches et ses commits](https://www.codeheroes.fr/2020/06/29/git-comment-nommer-ses-branches-et-ses-commits/).
