﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StrokeFirstAidLibrary.Entity;
using StrokeFirstAidLibrary.BLL;
using StrokeFirstAidLibrary.Util;

namespace StrokeFirstAidAPI.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ChoiceQuestionController : ControllerBase
    {
        [HttpGet, Route("{parentID?}")]
        public APIResult List(int parentID)
        {
            return new ChoiceQuestionBLL().List(parentID);
        }
    }
}
