# Migrer du code legacy vers une architecture héxagonale

Source d'exemple pour l'article d'architecture hexagonale. Le but est d'expliquer étape par étape le cheminement de la modification.

- _Etape 1 :_ [Extraction des dépendances](https://github.com/niccou/from-legacy-to-hexagonal/tree/feature/ExtractionDependances)

    La première étape constiste à sortir les dépendances de la partie à modifier.
 
 - _Etape 2 :_ [Ajout des tests unitaires](https://github.com/niccou/from-legacy-to-hexagonal/tree/feature/TestsUnitaires)
 
    Les tests vont permettre de faire les modifications tout en s'assurant de la non-régression du fonctionnement du code modifié.
