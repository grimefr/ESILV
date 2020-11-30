Pour pouvoir lancer le site internet il faudra avoir comme logiciel :
 
- MYSQL
-La dernière version de Python, téléchargeable en open source 
- Un logiciel d'éditeur de code (VS Code, notepade etc)


1) Placer le dossier Workare sur votre bureau
2) Lancez l'invité de commande
3) Faire la requête cd "chemin d'accès du dossier Workakre"
4) Faire "python manage.py makemigrations"
5) installer pip sur l'ordianateur si non présent (faire pip list pour tester sa présence)
7) python -m pip install --upgrade pip
8) faire "pip install django-crispy-forms"
9) télécharger de from https://www.lfd.uci.edu/~gohlke/pythonlibs/#mysqlclient le fichier "mysqlclient-1.4.6-cp38-cp38-win32.whl", puis le mettre dans le dossier projetV2. 
10) pip install mysqlclient-1.4.6-cp38-cp38-win32.whl
11) modifier le fichier settings.py dans le sous dossier ProjetV2, changez la ligne 85 : PASSWORD : "votre mot de passe liée à la base de donnée MYSQL"
12) pip install django-db
12) pip install mysql
13) Lancer Mysql Worbench, vous connecter, entré "create database MonSiteBDD" lancez puis :

alter user 'root'@'127.0.0.1' identified with mysql_native_password by 'votre mot de passe sql';
GRANT ALL PRIVILEGES ON *.* TO 'root'@'127.0.0.1' ;
flush privileges; 

Retourner dans le cmd : 
cd "chemin d'accès dossier Workare"
python manage.py makemigrations
python manage.py migrate
python manae.py runserver


Si une erreur persiste veuillez nous contacer par mail : francois.grime@edu.devinci.fr