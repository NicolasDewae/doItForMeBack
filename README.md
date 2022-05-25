# doItForMeBack
## Etapes pour installation:

### Vérifier si les packages suivants sont présent

- Microsoft.EntityFrameworkCore.Design (6.0.5)
- Microsoft.EntityFrameworkCore.SqlServer (6.0.5)
- Microsoft.EntityFrameworkCore.Tools (6.0.5)


### Effectuer et executer les migrations 
ouvrir la console:
outils -> Gestionnaire de package NuGet -> Console du Gestionnaire de package

Si besoin créer une migration tout en ajoutant un instantané de cette dernière.
```shell
add-migration migrationName
```

Executer la migration pour créer ou mettre à jour le schéma de la base de données.
```shell
update-database
```
ressources:

- [initialiser EF Core](https://dev.to/renukapatil/create-web-api-with-aspnet-core-60-46l4).
- [Commenter son code avec .Net](https://vincentlaine.developpez.com/tuto/dotnet/comdoc/#LIII-B-1).
- [Nommer ses branches et ses commits](https://www.codeheroes.fr/2020/06/29/git-comment-nommer-ses-branches-et-ses-commits/).
