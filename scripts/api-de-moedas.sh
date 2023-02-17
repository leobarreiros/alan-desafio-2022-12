#logar na EC2 via ssh no terminal do Ubuntu:
#copiar a chave de acesso para: \\wsl.localhost\Ubuntu\home\seu_user\.ssh
#cd .ssh 
#ssh -i testechave.pem ec2-user@ec2-18-230-110-23.sa-east-1.compute.amazonaws.com

sudo yum update -y
sudo rpm -Uvh https://packages.microsoft.com/config/centos/7/packages-microsoft-prod.rpm
sudo yum install aspnetcore-runtime-6.0
sudo mkdir /wwwroot
sudo mkdir /wwwroot/api-de-moedas
cd /wwwroot/api-de-moedas
sudo wget -O "apiZip" "https://codepipeline-sa-east-1-80672648724.s3.sa-east-1.amazonaws.com/alan-desafio-2022-12/applicatio/Cq490Ln?response-content-disposition=inline&X-Amz-Security-Token=IQoJb3JpZ2luX2VjEE4aCXNhLWVhc3QtMSJIMEYCIQDPnHVSHwmqvFaat87t1RRIR2n%2FiPnBgRzaYyyO5ZselQIhANRQDxTt%2BgmoLbBb63RuzcvgM8t0ga45FztdsYhHeLGoKvECCNf%2F%2F%2F%2F%2F%2F%2F%2F%2F%2FwEQABoMNTEzMTA3MTc4NTkxIgws6cIm3QDOgW1KvpQqxQL7fAUldeJ3fwmiTP9iuF3dyDr3a4Si%2F2YF1AqtYn9Yi0hZm2tQIBrtQnnwKzV43ENTYxzOcg9zNzqHHZJVnaLHldbAQ7sGeZ5JzDURa7eihP5%2FPkW4ssW9u%2Ft%2BozIGrGF5DeTtHZHfc9f%2BbuF%2BPHnCVwDTBiBF1PITs6tlXKdmzv1k5Vhf4wadeWpAlBUNwgi99VdhME%2FjSeSmp5f2uqmbZr9lQn8uzIXwfbqd9H2eK0PyhMB93kmzZbKb4mjHf3QL%2FcCnq3X7BXbBFeZka2ahiKoVQ6FxjrkH1rWSjyxHlkMMnIi4ygnx4Calk%2FGlYeDx1obyrHT%2B7AV8vNkr%2BN3LnIsMysdxzKDQajIUkcawpUVfm%2FxPwPv8ETObfsYucHbImxKr%2BnY9dY68VIpfEBr3sO2srl2ruVzXZzoQXo0Xp%2BIFMliJMNauo58GOrICELwhe0%2B5HHZCp9CN977q15aWSpqY73pBqkqXxLfZLRSXNvMVNdiFtHUhmup2U6V72H2QnrU%2FBYVKn9thK2NTI7wEHPfamjIAP33qY8EobFteawmwTrnKYDib4G%2B6mRJH3A1ZP5TvK8DfyI2rpFHGEJsIEGzj1dUW4fKWnwm6f1kPYrAV%2FHOK6bJisOxWkYtezHKWMFsAOHovtEZM0nJ9H6szg6FFGJnz7xGOVm%2F8euN1T3xuqvLvB%2FRv6PK0E54DoazqH4q6sfii3Othe6OaiUXuEaI9xSO5gXGfq4JdqrPWLFyOvlwpuLia%2Fl2RjNIGpmFNXd%2BfFHubEKuBYUkAM7pMjniU%2FGabYX0qG8Yq5ADvM831Jg3o7XT3fU30kk%2BzOP48VJ67JEtFHLaqoIiKhDM5&X-Amz-Algorithm=AWS4-HMAC-SHA256&X-Amz-Date=20230212T143814Z&X-Amz-SignedHeaders=host&X-Amz-Expires=1800&X-Amz-Credential=ASIAXO54SGRPXN2POENY%2F20230212%2Fsa-east-1%2Fs3%2Faws4_request&X-Amz-Signature=5b62b49acde79a8b5c9c1c56a17180c002191a043ada2bfdf87ab6d3ca95e44b"
sudo unzip apiZip
echo '[Unit]
Description= Api de Moedas running on Amazon Linux

[Service]
WorkingDirectory=wwwroot/api-de-moedas
ExecStart=/usr/bin/dotnet /wwwroot/api-de-moedas/ApiDeMoedas.dll --urls "http://0.0.0.0:80"
Restart=always
RestartSec=10
KillSignal=SIGINT
SyslogIdentifier=api-de-moedas
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target' | sudo tee /etc/systemd/system/apidemoedas.service
sudo systemctl stop apidemoedas.service
sudo systemctl stop apidemoedas.service
sudo systemctl start apidemoedas.service