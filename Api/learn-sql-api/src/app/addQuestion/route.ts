import { NextRequest, NextResponse } from "next/server";
import prisma from "@/lib/prisma";
import { Prisma } from "@prisma/client";

export async function POST(req:Request) {
     
    const que : QueData = await req.json();
    let ret;

    try{
        ret = await prisma.questionData.create({data : que});
        return NextResponse.json({ststus:"success",ret});
    }
    catch(e){
        return NextResponse.json({status:"fail",e});
    }
}