# doItForMeBack
## Etapes pour installation:

### Effectuer et executer les migrations 
Dans MySQL, créer une base de données nommée DoItForMeDatabase. si elle n'a pas ce nom il faudra changer le contenu de la ConnectionString dans le fichier appsettings.json

Ouvrir la console:
outils -> Gestionnaire de package NuGet -> Console du Gestionnaire de package

Executer la migration pour créer ou mettre à jour le schéma de la base de données.
```shell
update-database
```
Ressources:

- [initialiser EF Core](https://dev.to/renukapatil/create-web-api-with-aspnet-core-60-46l4).
- [Gérer l'authentification avec JWT](https://jasonwatmore.com/post/2021/12/14/net-6-jwt-authentication-tutorial-with-example-api);

- [Commenter son code avec .Net](https://vincentlaine.developpez.com/tuto/dotnet/comdoc/#LIII-B-1).
- [Nommer ses branches et ses commits](https://www.codeheroes.fr/2020/06/29/git-comment-nommer-ses-branches-et-ses-commits/).
