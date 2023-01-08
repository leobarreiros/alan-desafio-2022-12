#!/bin/bash
if test -d  /webapps/api-de-moedas; 
    then
        echo "/webapps/api-de-moedas exists";
    else
        echo "/webapps/api-de-moedas doesn't exist";
        exit 1;
fi

sudo chmod 664 /etc/systemd/system/api-de-moedas.service
sudo systemctl daemon-reload
sudo systemctl enable api-de-moedas
sudo systemctl start api-de-moedas