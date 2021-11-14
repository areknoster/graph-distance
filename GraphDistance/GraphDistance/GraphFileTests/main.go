package main

import (
	"fmt"
	"log"
	"math/rand"
	"os"
	"strconv"
	"strings"
)

func main() {
	if len(os.Args) != 3 {
		log.Fatalln("program size density")
	}
	size, density := os.Args[1], os.Args[2]
	sizeInt, err := strconv.Atoi(size)
	if err != nil {
		log.Fatalln("parse size")
	}
	densityFloat, err := strconv.ParseFloat(density, 64)
	if err != nil {
		log.Fatalln("parse float")
	}
	GenerateAndPrint(sizeInt, densityFloat)
}


func GenerateAndPrint(size int, density float64){
	fmt.Println(size)
	for i := 0; i < size; i++{
		sb := strings.Builder{}
		for j := 0 ; j < size; j++ {
			var b bool
			if i != j{
				b = rand.Float64() < density
			}
			if b {
				sb.WriteString("1 ")
			}else {
				sb.WriteString("0 ")
			}
		}
		fmt.Println(sb.String())
	}
}